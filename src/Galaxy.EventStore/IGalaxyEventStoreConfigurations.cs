using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using System;
using System.Threading.Tasks;

namespace Galaxy.EventStore
{
    public interface IGalaxyEventStoreConfigurations
    {
        IEventStoreConnection connection { get; set; }
        string username { get; set; }
        string password { get; set; }
        string uri { get; set; }
    }
   
}
