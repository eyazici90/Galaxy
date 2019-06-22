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

        bool Equals(Snapshot other)
        {
            if (ReferenceEquals(other, null)) return false;
            return _version == other._version && Equals(_state, other._state);
        } 
        public override bool Equals(object obj)
        {
            return Equals(obj as Snapshot);
        }
 
        public override int GetHashCode()
        {
            if (_state == null)
                return _version;
            return _version ^ _state.GetHashCode();
        }
    }
}
