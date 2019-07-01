using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.EventStore
{
    public class NullInstanceCheckpointStore : ICheckpointStore
    {
        public async Task<T> GetLastCheckpoint<T>(string projection) => await  Task.FromResult(default(T));

        public async Task SetLastCheckpoint<T>(string projection, T checkpoint) => await Task.FromResult(true);
       
    }
}
