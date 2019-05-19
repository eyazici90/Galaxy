using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Domain
{
    public interface ISnapshotable
    {
        void RestoreSnapshot(object state);
         
        object TakeSnapshot();
    }
}
