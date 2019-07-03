using EventStore.ClientAPI;
using Galaxy.Serialization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.EventStore
{
    public class SubscriptionManagerBuilder
    {
        public static readonly SubscriptionManagerBuilder New = new SubscriptionManagerBuilder();
        private ICheckpointStore _checkpointStore;
        private IEventStoreConnection _connection;
        private ISerializer _deserializer;
        private int? _maxLiveQueueSize;
        private ProjectionHandler[] _projections;
        private int? _readBatchSize;
        private ISnapshotter[] _snapshotters = { };

        public SubscriptionManagerBuilder Connection(IEventStoreConnection connection)
        {
            _connection = connection;
            return this;
        }

        public SubscriptionManagerBuilder Deserializer(ISerializer deserializer)
        {
            _deserializer = deserializer;
            return this;
        }

        public SubscriptionManagerBuilder MaxLiveQueueSize(int maxLiveQueueSize)
        {
            _maxLiveQueueSize = maxLiveQueueSize;
            return this;
        }

        public SubscriptionManagerBuilder CheckpointStore(ICheckpointStore checkpointStore)
        {
            _checkpointStore = checkpointStore;
            return this;
        }

        public SubscriptionManagerBuilder ReadBatchSize(int readBatchSize)
        {
            _readBatchSize = readBatchSize;
            return this;
        }

        public SubscriptionManagerBuilder Snaphotter(params ISnapshotter[] snapshotters)
        {
            _snapshotters = snapshotters;
            return this;
        }

        public SubscriptionManagerBuilder Projections(params ProjectionHandler[] projections)
        {
            _projections = projections;
            return this;
        }

        public SubscriptionManager Build() =>
          new SubscriptionManager(_connection, _deserializer, _checkpointStore, _projections, _snapshotters, _maxLiveQueueSize, _readBatchSize);

        public async Task<SubscriptionManager> Activate()
        {
            SubscriptionManager manager = Build();
            await manager.Activate();
            return manager;
        }

        
    }
}
