using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Events
{
    public interface IEventBus
    {
        Task Publish(IntegrationEvent @event);

        Task Publish<TEvent>(TEvent @event) where TEvent : IntegrationEvent;

        Task Publish<TEvent>(object @event) where TEvent : IntegrationEvent;
    }
}
