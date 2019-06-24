using EventStore.ClientAPI;
using Galaxy.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.EventStore
{
    public class SnapshotReaderAsync : IAsyncSnapshotReader
    {
        private readonly IEventStoreConnection _connection;
        private readonly ISerializer _serializer; 
         
        public SnapshotReaderAsync(IEventStoreConnection connection, ISerializer serializer)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection)); 
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }
  
 
        public async Task<Optional<Snapshot>>  ReadOptional(string identifier)
        {
            if (identifier == null) throw new ArgumentNullException(nameof(identifier)); 
            var slice = await _connection.
                ReadStreamEventsBackwardAsync(
                    identifier, StreamPosition.End, 1, false);

            if (slice.Status == SliceReadStatus.StreamDeleted || slice.Status == SliceReadStatus.StreamNotFound ||
                (slice.Events.Length == 0 && slice.NextEventNumber == -1))
            {
                return Optional<Snapshot>.Empty;
            }
            var e = slice.Events[0].Event;
            var eData = this._serializer.Deserialize(Type.GetType(e.EventType, true)
                                                        , Encoding.UTF8.GetString(e.Data));

            var eMetaData = this._serializer.Deserialize<EventMetadata>( Encoding.UTF8.GetString(e.Metadata));

            return new Optional<Snapshot>( new Snapshot(eMetaData.Version, eData));
        }
    }
}
