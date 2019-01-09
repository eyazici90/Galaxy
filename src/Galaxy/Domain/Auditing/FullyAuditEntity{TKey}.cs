using Galaxy.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Auditing
{
    public abstract class FullyAuditEntity<TPrimaryKey> : Entity<TPrimaryKey>, IFullyAudit, ISoftDelete
    {
        public virtual bool IsDeleted { get; protected set; }
        public virtual int? TenantId { get; protected set; }
        public virtual int? CreatorUserId { get; protected set; }
        public virtual DateTime? LastModificationTime { get; protected set; }
        public virtual int? LastModifierUserId { get; protected set; }
        public virtual DateTime? CreationTime { get; protected set; }

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

        public void SyncAuditState(int? creatorUserId = null, DateTime? lastModificationTime = null, int? lastmodifierUserId = null, DateTime? creationTime = null)
        {
            this.IsDeleted = IsDeleted;
            
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
