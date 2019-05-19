using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Azure.ServiceBus
{
    public static class GalaxyAzureServiceBusFactory
    {
        public static GalaxyAzureServiceBus Create(Action<IGalaxyAzureServiceBusConfigurations> configurations)
        {
            var configs = new GalaxyAzureServiceBusConfigurations();

            configurations(configs);

            var topicClient = new TopicClient(configs.ServiceBusConnectionString, configs.TopicName);

            var queueClient = new QueueClient(configs.ServiceBusConnectionString, configs.QueueName);

            return new GalaxyAzureServiceBus(topicClient, queueClient);
        }
    }
}
