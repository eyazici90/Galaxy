using Galaxy.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.RabbitMQ
{
    public class GalaxyEventBusRabbitMQ : IEventBus
    {
        private readonly IBus _bus;
        private readonly IGalaxyRabbitMQConfiguration _galaxyRabbitMqConfiguration;
        public GalaxyEventBusRabbitMQ(IBus bus
            , IGalaxyRabbitMQConfiguration galaxyRabbitMqConfiguration)
        {
            _bus = bus ?? throw new ArgumentNullException (nameof(bus));
            _galaxyRabbitMqConfiguration = galaxyRabbitMqConfiguration ?? throw new ArgumentNullException(nameof(galaxyRabbitMqConfiguration));
        }

        public async Task Publish(object @event)
        {
            await _bus.Publish(@event);
        }

        public async Task Publish<TEvent>(object @event) where TEvent : class
        {
            await _bus.Publish<TEvent>(@event);
        }

    
    }
}
