using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.EventStore
{
    public class Snapshot
    {
        readonly int _version;
        readonly object _state;
         
        public Snapshot(int version, object state)
        {
            _version = version;
            _state = state;
        }
         
        public int Version
        {
            get { return _version; }
        }
         
        public object State
        {
            get { return _state; }
        }
 
    }
}
