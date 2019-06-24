using EventStore.ClientAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.EventStore
{
    public class GalaxyEventStoreConfigurations : IGalaxyEventStoreConfigurations
    {
        public IEventStoreConnection Connection { get; set; }
         public string Username { get; set; }
        public string Password { get; set; }
        public string Uri { get; set; }
        public bool IsSnapshottingOn { get; set; }
    }
}
