using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.EventStore
{
    public interface ISnapshotReader
    {
        Optional<Snapshot> ReadOptional(string identifier);
    }
}
