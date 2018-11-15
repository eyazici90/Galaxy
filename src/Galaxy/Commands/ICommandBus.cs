using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Commands
{
    public interface ICommandBus
    {
        Task Send(IntegrationCommand command);

        Task Send<TCommand>(IntegrationCommand command) where TCommand : IntegrationCommand;

        Task Send<TCommand>(object command) where TCommand : IntegrationCommand;
        
    }
}
