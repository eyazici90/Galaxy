using Galaxy.Commands;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.RabbitMQ
{
    public class GalaxyCommandBusRabbitMQ : ICommandBus
    {
        private readonly IBus _bus;
        private readonly IGalaxyRabbitMQConfiguration _galaxyRabbitMqConfiguration;
        public GalaxyCommandBusRabbitMQ(IBus bus
            , IGalaxyRabbitMQConfiguration galaxyRabbitMqConfiguration)
        {
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
            _galaxyRabbitMqConfiguration = galaxyRabbitMqConfiguration ?? throw new ArgumentNullException(nameof(galaxyRabbitMqConfiguration));
        } 

        public async Task SendAsync(object command)
        {
            await _bus.Send(command);
        }

        public async Task SendAsync<TCommand>(object command) where TCommand : class
        {
            await _bus.Send<TCommand>(command);
        }
    }
}
