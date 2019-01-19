using Galaxy.Auditing;
using Galaxy.Domain;
using Galaxy.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Galaxy.Identity.Domain
{
    
    public abstract class FullyAuditIdentityUserEntity<TPrimaryKey> : IdentityUser<TPrimaryKey>, IFullyAudit<TPrimaryKey>, IEntity<TPrimaryKey>, IObjectState where TPrimaryKey : struct, IEquatable<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get; protected set; }
        public virtual bool IsDeleted { get; protected set; }
        public virtual int? TenantId { get; protected set; }
        public virtual int? CreatorUserId { get; protected set; }
        public virtual DateTime? LastModificationTime { get; protected set; }
        public virtual int? LastModifierUserId { get; protected set; }
        public virtual DateTime? CreationTime { get; protected set; }
        private IEventRouter _eventRouter;

        public FullyAuditIdentityUserEntity()
        {
            _eventRouter = new InstanceEventRouter();
        }
    
        private List<INotification> _domainEvents;

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public virtual void RemoveEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public virtual void ClearEvents()
        {
            _domainEvents?.Clear();
        }

        public virtual void RegisterEvent<TEvent>(Action<TEvent> handler)
        {
            _eventRouter.Register<TEvent>(handler);
        }

        public virtual void ApplyEvent(object @event)
        {
            if (@event == null)
            {
                throw new ArgumentNullException(nameof(@event));
            }
            _eventRouter.Route(@event);
            AddEvent(@event as INotification);
        }

        public virtual void ApplyAllChanges()
        {
            foreach (var @event in DomainEvents)
            {
                _eventRouter.Route(@event);
            }
        }
        
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

        public void SyncAuditState(int? creatorUserId = null, DateTime? lastModificationTime = null, int? lastmodifierUserId = null, DateTime? creationTime = null)
        {
            
            if (creatorUserId.HasValue)
                this.CreatorUserId = creatorUserId;

            if (lastModificationTime.HasValue)
                this.LastModificationTime = lastModificationTime;

            if (lastmodifierUserId.HasValue)
                this.LastModifierUserId = lastmodifierUserId;

            if (creationTime.HasValue)
                this.CreationTime = creationTime;
        }

        public void SyncTenantState(int? tenantId = null)
        {
            if (tenantId.HasValue)
                this.TenantId = tenantId;
        }
    }
}
