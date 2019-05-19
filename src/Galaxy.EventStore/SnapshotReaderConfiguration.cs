using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.EventStore
{
    public class SnapshotReaderConfiguration
    {
        readonly ISnapshotDeserializer _deserializer;
        readonly IStreamNameResolver _streamNameResolver;
        readonly IStreamUserCredentialsResolver _streamUserCredentialsResolver;

        public SnapshotReaderConfiguration(ISnapshotDeserializer deserializer, IStreamNameResolver streamNameResolver,
                                           IStreamUserCredentialsResolver streamUserCredentialsResolver)
        { 
            _deserializer = deserializer ?? throw new ArgumentNullException(nameof(deserializer));
            _streamNameResolver = streamNameResolver ?? throw new ArgumentNullException(nameof(streamNameResolver)); ;
            _streamUserCredentialsResolver = streamUserCredentialsResolver ?? throw new ArgumentNullException(nameof(StreamUserCredentialsResolver)); ;
        }


        public ISnapshotDeserializer Deserializer
        {
            get { return _deserializer; }
        }

        public IStreamNameResolver StreamNameResolver
        {
            get { return _streamNameResolver; }
        }

    
        public IStreamUserCredentialsResolver StreamUserCredentialsResolver
        {
            get { return _streamUserCredentialsResolver; }
        }
    }
}
