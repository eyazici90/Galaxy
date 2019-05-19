using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Commands
{
    public static class CommandBusExtensions
    {
        public static void Send<TCommand>(this ICommandBus commandBus, TCommand commandData)
         where TCommand : class
        {
            SendAsync(commandBus, commandData).ConfigureAwait(false)
                .GetAwaiter().GetResult();
        }

        public static void Send(this ICommandBus commandBus, Type commandType, object commandData)
        {
            SendAsync(commandBus, commandData).ConfigureAwait(false)
               .GetAwaiter().GetResult();
        }

        public static async Task SendAsync<TCommand>(this ICommandBus commandBus, TCommand commandData)
            where TCommand : class
        {
            if (commandData == null) throw new ArgumentNullException(nameof(commandData));

            await commandBus.SendAsync(commandData);
        }

        public static async Task SendAsync(this ICommandBus commandBus, Type commandType, object commandData)
        {
            if (commandData == null) throw new ArgumentNullException(nameof(commandData));

            await commandBus.SendAsync(commandType, commandData);
        }
    }
}
