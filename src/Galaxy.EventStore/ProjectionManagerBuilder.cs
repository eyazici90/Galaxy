using EventStore.ClientAPI;
using Galaxy.Serialization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.EventStore
{
    public class ProjectionManagerBuilder
    {
        public static readonly ProjectionManagerBuilder With = new ProjectionManagerBuilder();
        private ICheckpointStore _checkpointStore;
        private IEventStoreConnection _connection;
        private ISerializer _deserializer;
        private int? _maxLiveQueueSize;
        private IProjection<INotification>[] _projections;
        private int? _readBatchSize;
        private ISnapshotter[] _snapshotters = { };

        public ProjectionManagerBuilder Connection(IEventStoreConnection connection)
        {
            _connection = connection;
            return this;
        }

        public ProjectionManagerBuilder Deserializer(ISerializer deserializer)
        {
            _deserializer = deserializer;
            return this;
        }

        public ProjectionManagerBuilder MaxLiveQueueSize(int maxLiveQueueSize)
        {
            _maxLiveQueueSize = maxLiveQueueSize;
            return this;
        }

        public ProjectionManagerBuilder CheckpointStore(ICheckpointStore checkpointStore)
        {
            _checkpointStore = checkpointStore;
            return this;
        }

        public ProjectionManagerBuilder ReadBatchSize(int readBatchSize)
        {
            _readBatchSize = readBatchSize;
            return this;
        }

        public ProjectionManagerBuilder Snaphotter(params ISnapshotter[] snapshotters)
        {
            _snapshotters = snapshotters;
            return this;
        }

        public ProjectionManagerBuilder Projections(params IProjection<INotification>[] projections)
        {
            _projections = projections;
            return this;
        }

        public ProjectionManager<TConnection> Build<TConnection>(Func<TConnection> connection) =>
          new ProjectionManager<TConnection>(_connection, _deserializer, connection, _checkpointStore, _projections, _snapshotters, _maxLiveQueueSize, _readBatchSize);

        public async Task<ProjectionManager<TConnection>> Activate<TConnection>(Func<TConnection> getConnection)
        {
            ProjectionManager<TConnection> manager = Build(getConnection);
            await manager.Activate();
            return manager;
        }

        
    }
}
