using EventStore.ClientAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.EventStore
{
    public interface ISnapshotter
    {
        bool ShouldTakeSnapshot(Type aggregateType, ResolvedEvent e);

        Task TakeSnapshot(string stream);
    }
}
