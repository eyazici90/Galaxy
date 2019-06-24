using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using System;
using System.Threading.Tasks;

namespace Galaxy.EventStore
{
    public interface IGalaxyEventStoreConfigurations
    {
        IEventStoreConnection Connection { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string Uri { get; set; }
        bool IsSnapshottingOn { get; set; }
    }
   
}
