using Galaxy.Auditing;
using Galaxy.Domain;
using Galaxy.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Galaxy.Identity
{
   
    public abstract class FullyAuditIdentityRoleEntity<TPrimaryKey> : IdentityRole<TPrimaryKey>, IFullyAudit<TPrimaryKey>, IEntity<TPrimaryKey>, IObjectState where TPrimaryKey : struct, IEquatable<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get; protected set; }
        public virtual bool IsDeleted { get; protected set; }
        public virtual int? TenantId { get; protected set; }
        public virtual int? CreatorUserId { get; protected set; }
        public virtual DateTime? LastModificationTime { get; protected set; }
        public virtual int? LastModifierUserId { get; protected set; }
        public virtual DateTime? CreationTime { get; protected set; }

        private List<INotification> _domainEvents;
        private IEventRouter _eventRouter;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public FullyAuditIdentityRoleEntity()
        {
            _eventRouter = new InstanceEventRouter();
        }

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

        public virtual void SyncAuditState(int? tenantId = default, int? creatorUserId = default, DateTime? lastModificationTime = default, int? lastmodifierUserId = default, DateTime? creationTime = default)
        {
            this.IsDeleted = IsDeleted;
            this.TenantId = tenantId;
            this.CreatorUserId = creatorUserId;
            this.LastModificationTime = lastModificationTime;
            this.LastModifierUserId = lastmodifierUserId;
            this.CreationTime = creationTime;
        }

        public void SyncAuditState(int? creatorUserId = null, DateTime? lastModificationTime = null, int? lastmodifierUserId = null, DateTime? creationTime = null)
        {
            this.IsDeleted = IsDeleted; 
            this.CreatorUserId = creatorUserId;
            this.LastModificationTime = lastModificationTime;
            this.LastModifierUserId = lastmodifierUserId;
            this.CreationTime = creationTime;
        }
    }
}
