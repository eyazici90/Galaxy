using Galaxy.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Galaxy.Domain
{
    public abstract class AggregateRootEntity<TPrimaryKey> : IAggregateRoot, IObjectState, IEntity<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get; protected set; }

        private List<INotification> _domainEvents;

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public virtual void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public virtual void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        [NotMapped]
        public virtual ObjectState ObjectState { get; private set; }


        public virtual void SyncObjectState(ObjectState objectState)
        {
            this.ObjectState = objectState;
        }

        public virtual bool IsTransient()
        {
            if (EqualityComparer<TPrimaryKey>.Default.Equals(Id, default(TPrimaryKey)))
            {
                return true;
            }
            if (typeof(int) == typeof(int))
            {
                return Convert.ToInt32(Id) <= 0;
            }
            if (typeof(int) == typeof(long))
            {
                return Convert.ToInt64(Id) <= 0;
            }
            return false;
        }
    }
}

