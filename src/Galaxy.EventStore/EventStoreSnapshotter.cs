using EventStore.ClientAPI;
using Galaxy.Domain;
using Galaxy.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.EventStore
{
    public class EventStoreSnapshotter<TAggregate, TSnapshot> : ISnapshotter where TAggregate : Aggregate
    {
        private readonly Func<string, Task<TAggregate>> _getAggreagate;
        private readonly Func<IEventStoreConnection> _getConnection; 
        private readonly Func<string, string> _snapshotNameResolve;
        private readonly Func<ResolvedEvent, bool> _strategy;
        private readonly ISerializer _serializer;

        public EventStoreSnapshotter(
            Func<string, Task<TAggregate>> getAggreagate,
            Func<IEventStoreConnection> getConnection,
            Func<ResolvedEvent, bool> strategy,
            Func<string, string> snapshotNameResolve,
            ISerializer serializer
            )
        {
            _strategy = strategy;
            _snapshotNameResolve = snapshotNameResolve; 
            _getConnection = getConnection;
            _getAggreagate = getAggreagate;
            _serializer = serializer;
        }

        public bool ShouldTakeSnapshot(Type aggregateType, ResolvedEvent e) =>
            typeof(ISnapshotable).IsAssignableFrom(aggregateType) && _strategy(e);

        public async Task TakeSnapshot(string stream)
        {
            TAggregate aggregate = await _getAggreagate(stream);

            EventData[] changes = (aggregate.Root as IEntity).DomainEvents
                                                 .Select(@event => new EventData(
                                                     Guid.NewGuid(),
                                                     @event.GetType().FullName,
                                                     true,
                                                     Encoding.UTF8.GetBytes(this._serializer.Serialize(@event)),
                                                     Encoding.UTF8.GetBytes(this._serializer.Serialize(new EventMetadata
                                                     {
                                                         TimeStamp = DateTime.Now,
                                                         AggregateType = aggregate.GetType().Name,
                                                         AggregateAssemblyQualifiedName = aggregate.GetType().AssemblyQualifiedName,
                                                         IsSnapshot = false
                                                     }))
                                                     )).ToArray();

            string snapshotStream = _snapshotNameResolve(stream);
            await _getConnection().AppendToStreamAsync(snapshotStream, ExpectedVersion.Any, changes);
        }

       
    }
}
