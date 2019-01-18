using Galaxy.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Domain.Auditing
{
    public abstract class FullyAuditAggregateRootEntity<TPrimaryKey> : AuditAggregateRootEntity<TPrimaryKey>, IAggregateRoot, IEntity<TPrimaryKey>, IFullyAudit, ISoftDelete
    {
        public virtual int? TenantId { get; protected set; }

        public virtual void SyncAuditState(int? tenantId = default, int? creatorUserId = default, DateTime? lastModificationTime = default, int? lastmodifierUserId = default, DateTime? creationTime = default)
        {
            this.IsDeleted = IsDeleted;

            if (tenantId.HasValue)
                this.TenantId = tenantId;

            if (creatorUserId.HasValue)
                this.CreatorUserId = creatorUserId;

            if (lastModificationTime.HasValue)
                this.LastModificationTime = lastModificationTime;

            if (lastmodifierUserId.HasValue)
                this.LastModifierUserId = lastmodifierUserId;

            if (creationTime.HasValue)
                this.CreationTime = creationTime;
        }
    }
}
