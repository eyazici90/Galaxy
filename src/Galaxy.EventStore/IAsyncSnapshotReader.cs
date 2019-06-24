using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.EventStore
{
    public interface IAsyncSnapshotReader
    {
        Task<Optional<Snapshot>>  ReadOptional(string identifier);
    }
}
