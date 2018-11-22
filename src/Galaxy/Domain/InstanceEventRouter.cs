using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Domain
{
    public class InstanceEventRouter : IEventRouter
    {
        private readonly Dictionary<Type, Action<object>> _handlers;

        public InstanceEventRouter() => _handlers = new Dictionary<Type, Action<object>>();
        
        private void ConfigureRoute(Type @event, Action<object> handler)
        {
            if (@event == null)
            {
                throw new ArgumentNullException(nameof(@event));
            }
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            _handlers.Add(@event, handler);
        }
        
        private void ConfigureRoute<TEvent>(Action<TEvent> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            _handlers.Add(typeof(TEvent), @event => handler((TEvent)@event));
        }

        public void Register<TEvent>(Action<TEvent> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            this.ConfigureRoute(handler);
        }

        public void Register(Type @event, Action<object> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            this.ConfigureRoute(@event, handler);
        }

        public void Route(object @event)
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event));
        
            Action<object> handler;
            if (_handlers.TryGetValue(@event.GetType(), out handler))
            {
                handler(@event);
            }
        }

        public void Apply(object @event)
        {
            this.Route(@event);
        }
    }
}
