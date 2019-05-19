using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Events
{
    public interface IEventBus
    {
        Task PublishAsync(object @event);

        Task PublishAsync<TEvent>(object @event) where TEvent : class;
    }
}
