using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Commands
{
    public class IntegrationCommand : ICommand
    {
        public IntegrationCommand()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public Guid Id { get; }
        public DateTime CreationDate { get; }
    }
}
