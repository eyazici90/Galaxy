using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Events
{
    public static class EventBusExtensions
    {
        public static void Publish<TEvent>(this IEventBus eventBus, TEvent eventData)
           where TEvent : class
        {
            PublishAsync(eventBus, eventData).ConfigureAwait(false)
                .GetAwaiter().GetResult();
        }

        public static void Publish(this IEventBus eventBus, Type eventType, object eventData)
        {
            PublishAsync(eventBus, eventData).ConfigureAwait(false)
               .GetAwaiter().GetResult();
        }

        public static async Task PublishAsync<TEvent>(this IEventBus eventBus, TEvent eventData)
            where TEvent : class
        {
            if (eventData == null) throw new ArgumentNullException(nameof(eventData));

            await eventBus.PublishAsync(eventData);
        }

        public static async Task PublishAsync(this IEventBus eventBus, Type eventType, object eventData)
        {
            if (eventData == null) throw new ArgumentNullException(nameof(eventData));

            await eventBus.PublishAsync(eventType, eventData);
        }
    }
}
