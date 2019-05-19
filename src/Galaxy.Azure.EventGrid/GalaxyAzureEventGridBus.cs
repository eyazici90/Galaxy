using Galaxy.Events;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Azure.EventGrid
{
    public class GalaxyAzureEventGridBus : IEventBus
    {
        private readonly IEventGridClient _eventGridClient;
        private readonly IGalaxyAzureEventGridConfigurations _configurations;
        private readonly string _topicHostName;
        public GalaxyAzureEventGridBus(IEventGridClient eventGridClient, IGalaxyAzureEventGridConfigurations configurations)
        {
            _configurations = configurations ?? throw new ArgumentNullException(nameof(configurations));

            _eventGridClient = eventGridClient ?? throw new ArgumentNullException(nameof(eventGridClient));

            _topicHostName = new Uri(configurations.TopicUrl).Host;
        } 

        public async Task PublishAsync(object @eventList)
        {
            if (eventList == null) throw new ArgumentNullException(nameof(eventList));

            await _eventGridClient.PublishEventsAsync(_topicHostName, @eventList as IList<EventGridEvent>);
        }

        public async Task PublishAsync<TEvent>(object @eventList) where TEvent : class
        {
            if (eventList == null) throw new ArgumentNullException(nameof(eventList));

            await _eventGridClient.PublishEventsAsync(_topicHostName, @eventList as IList<EventGridEvent>);
        }
    }
}
