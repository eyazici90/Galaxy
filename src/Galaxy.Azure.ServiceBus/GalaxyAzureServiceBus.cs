using Galaxy.Commands;
using Galaxy.Events;
using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Azure.ServiceBus
{
    public class GalaxyAzureServiceBus : IEventBus, ICommandBus
    {
        private readonly ITopicClient _topicClient;
        private readonly IQueueClient _queueClient;
        public GalaxyAzureServiceBus(ITopicClient topicClient, IQueueClient queueClient)
        {
            _topicClient = topicClient ?? throw new ArgumentNullException(nameof(topicClient));
            _queueClient = queueClient ?? throw new ArgumentNullException(nameof(queueClient));
        }

        public async Task PublishAsync(object @event)
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));

            await _topicClient.SendAsync(@event as Message);
        }

        public async Task PublishAsync<TEvent>(object @event) where TEvent : class
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));

            await _topicClient.SendAsync(@event as Message);
        }

        public async Task SendAsync(object command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            await _queueClient.SendAsync(command as Message);
        }

        public async Task SendAsync<TCommand>(object command) where TCommand : class
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            await _queueClient.SendAsync(command as Message);
        }
    }
}
