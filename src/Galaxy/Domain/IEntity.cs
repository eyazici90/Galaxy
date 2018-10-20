using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Domain
{
   public interface IEntity : IEntity<int> 
    {
        IReadOnlyCollection<INotification> DomainEvents { get; }
        void AddDomainEvent(INotification eventItem);
        void RemoveDomainEvent(INotification eventItem);
        void ClearDomainEvents();
    }
}
