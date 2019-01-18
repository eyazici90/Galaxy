using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Domain.Auditing
{
     
    public abstract class AuditAggregateRootEntity<TPrimaryKey> : AggregateRootEntity<TPrimaryKey>, IAggregateRoot, IEntity<TPrimaryKey>, IAudit
    {
        public virtual int? CreatorUserId { get; protected set; }
        public virtual DateTime? LastModificationTime { get; protected set; }
        public virtual int? LastModifierUserId { get; protected set; }
        public virtual DateTime? CreationTime { get; protected set; }

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
    }
}
