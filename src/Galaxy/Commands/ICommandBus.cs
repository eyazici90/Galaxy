using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Commands
{
    public interface ICommandBus
    { 
        Task SendAsync(object command);

        Task SendAsync<TCommand>(object command) where TCommand : class;

    }
}
