using Galaxy.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Domain
{
   public interface IEntity: IObjectState
    {
        IReadOnlyCollection<INotification> DomainEvents { get; }
        void AddEvent(INotification eventItem);
        void RemoveEvent(INotification eventItem);
        void ClearEvents();
        void RegisterEvent<TEvent>(Action<TEvent> handler);
        void ApplyEvent(object @event);
        void ApplyAllChanges();
    }
}
