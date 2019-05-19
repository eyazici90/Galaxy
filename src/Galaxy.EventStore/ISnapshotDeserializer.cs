using EventStore.ClientAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.EventStore
{
    public interface ISnapshotDeserializer
    {
        Snapshot Deserialize(ResolvedEvent resolvedEvent);
    }
}
