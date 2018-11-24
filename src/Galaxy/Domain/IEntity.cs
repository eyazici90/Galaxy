using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Domain
{
   public interface IEntity 
    {
        IReadOnlyCollection<INotification> DomainEvents { get; }
        void AddDomainEvent(INotification eventItem);
        void RemoveDomainEvent(INotification eventItem);
        void ClearDomainEvents();
        void Register<TEvent>(Action<TEvent> handler);
        void ApplyDomainEvent(object @event);
        void ApplyAllChanges();
    }
}
