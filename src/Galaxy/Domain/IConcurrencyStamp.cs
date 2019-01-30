using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Domain
{
    public interface IConcurrencyStamp
    {
        string ConcurrencyStamp { get; }

        void SyncConcurrencyStamp(string stamp);
    }
}
