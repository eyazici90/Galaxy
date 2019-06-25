using EventStore.ClientAPI;
using Galaxy.Serialization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Galaxy.EventStore
{
    public class SubscriptionManager
    { 
        private readonly ICheckpointStore _checkpointStore;
        private readonly IEventStoreConnection _connection;
        private readonly int _maxLiveQueueSize;
        private readonly Projection[] _projections;
        private readonly int _readBatchSize;
        private readonly ISerializer _serializer;
        private readonly ISnapshotter[] _snapshotters;

        internal SubscriptionManager(IEventStoreConnection connection,
            ISerializer serializer,
            ICheckpointStore checkpointStore,
            Projection[] projections,
            ISnapshotter[] snapshotters,
            int? maxLiveQueueSize,
            int? readBatchSize)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            _projections = projections;
            _snapshotters = snapshotters;
            _checkpointStore = checkpointStore;
            _maxLiveQueueSize = maxLiveQueueSize ?? 10000;
            _readBatchSize = readBatchSize ?? 500;
        }

        public Task Activate() => Task.WhenAll(_projections.Select(x => StartProjection(x)));

        private async Task StartProjection(Projection projection)
        {
            var projectionTypeName = projection.GetType().FullName;

            Position lastCheckpoint = await _checkpointStore.GetLastCheckpoint<Position>(projectionTypeName);

            var settings = new CatchUpSubscriptionSettings(
                _maxLiveQueueSize,
                _readBatchSize,
                false,
                false,
                projectionTypeName
            );

            _connection.SubscribeToAllFrom(
                lastCheckpoint,
                settings,
                EventAppeared(projection, projectionTypeName),
                LiveProcessingStarted(projection, projectionTypeName),
                SubscriptionDropped(projection, projectionTypeName)
                );
        }
 
        private Func<EventStoreCatchUpSubscription, ResolvedEvent, Task> EventAppeared(
            Projection projection,
            string projectionName
        ) => async (_, e) =>
        {
            // check system event
            if (e.OriginalEvent.EventType.StartsWith("$")) { return; }

            var @event = this._serializer.Deserialize(Type.GetType(e.Event.EventType), Encoding.UTF8.GetString(e.Event.Data));

            if (@event == null) { throw new ArgumentNullException(nameof(@event)); }
            
            await projection.Handle(@event);  
             
            await _checkpointStore.SetLastCheckpoint(projectionName, e.OriginalPosition);

            var metadata = this._serializer.Deserialize<EventMetadata>(Encoding.UTF8.GetString(e.Event.Metadata));

            ISnapshotter snapshotter = _snapshotters.FirstOrDefault(
                            x => x.ShouldTakeSnapshot(Type.GetType(metadata.AggregateAssemblyQualifiedName), e) && !metadata.IsSnapshot);

            if (snapshotter != null)
            {
                await snapshotter.TakeSnapshot(e.OriginalStreamId);
            }
        };

        private Action<EventStoreCatchUpSubscription, SubscriptionDropReason, Exception> SubscriptionDropped(Projection projection, string projectionName)
            => (subscription, reason, ex) =>
            { 
                subscription.Stop();

                switch (reason)
                {
                    case SubscriptionDropReason.UserInitiated: 
                        break;
                    case SubscriptionDropReason.SubscribingError:
                    case SubscriptionDropReason.ServerError:
                    case SubscriptionDropReason.ConnectionClosed:
                    case SubscriptionDropReason.CatchUpError:
                    case SubscriptionDropReason.ProcessingQueueOverflow:
                    case SubscriptionDropReason.EventHandlerException: 
                        Task.Run(() => StartProjection(projection));
                        break;
                    default: 
                        break;
                }
            };

        private static Action<EventStoreCatchUpSubscription> LiveProcessingStarted(Projection projection, string projectionName)
            => _ => 
            {

            };
    }
}
