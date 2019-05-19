using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Azure.ServiceBus
{
    public class GalaxyAzureServiceBusConfigurations : IGalaxyAzureServiceBusConfigurations
    {
        public string ServiceBusConnectionString { get; set; }
        public string TopicName { get; set; }
        public string QueueName { get; set; }
    }
}
