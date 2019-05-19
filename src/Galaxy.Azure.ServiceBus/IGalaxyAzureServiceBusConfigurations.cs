using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Azure.ServiceBus
{
    public interface IGalaxyAzureServiceBusConfigurations
    {
        string ServiceBusConnectionString { get; set; }
        string TopicName { get; set; }
        string QueueName { get; set; }
    }
}
