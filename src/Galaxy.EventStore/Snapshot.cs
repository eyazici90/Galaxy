using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.EventStore
{
    public class Snapshot
    {
        public Snapshot(int version, object state)
        {
            Version = version;
            State = state;
        }

        public int Version { get; }

        public object State { get; }

    }
}
