using EventStore.ClientAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.EventStore
{
    public class GalaxyEventStoreConfigurations : IGalaxyEventStoreConfigurations
    {
        public IEventStoreConnection connection { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string uri { get; set; }
    }
}
