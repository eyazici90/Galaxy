using EventStore.ClientAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.EventStore
{
    public class SnapshotReader : ISnapshotReader
    {
        private readonly IEventStoreConnection _connection;
        private readonly SnapshotReaderConfiguration _configuration;
         
        public SnapshotReader(IEventStoreConnection connection, SnapshotReaderConfiguration configuration)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
 
        public IEventStoreConnection Connection
        {
            get { return _connection; }
        }
 
        public SnapshotReaderConfiguration Configuration
        {
            get { return _configuration; }
        }
 
        public Optional<Snapshot> ReadOptional(string identifier)
        {
            if (identifier == null) throw new ArgumentNullException(nameof(identifier));
            var streamUserCredentials = _configuration.StreamUserCredentialsResolver.Resolve(identifier);
            var streamName = Configuration.StreamNameResolver.Resolve(identifier);
            var slice = Connection.
                ReadStreamEventsBackwardAsync(
                    streamName, StreamPosition.End, 1, false, streamUserCredentials).
                Result;
            if (slice.Status == SliceReadStatus.StreamDeleted || slice.Status == SliceReadStatus.StreamNotFound ||
                (slice.Events.Length == 0 && slice.NextEventNumber == -1))
            {
                return Optional<Snapshot>.Empty;
            }
            return new Optional<Snapshot>(Configuration.Deserializer.Deserialize(slice.Events[0]));
        }
    }
}
