using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Domain
{
    public interface IEventRouter
    {
        void Register<TEvent>(Action<TEvent> handler);

        void Route(object @event);
    }
}
