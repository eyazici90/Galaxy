using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Events
{
    public interface IEventBus
    {
        Task Publish(object @event);

        Task Publish<TEvent>(object @event) where TEvent : class;
    }
}
