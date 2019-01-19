using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Domain.Auditing
{ 
    public abstract class AuditEntity<TPrimaryKey> : Entity<TPrimaryKey>, IEntity<TPrimaryKey>, IAudit 
    {
        public virtual int? CreatorUserId { get; protected set; }
        public virtual DateTime? LastModificationTime { get; protected set; }
        public virtual int? LastModifierUserId { get; protected set; }
        public virtual DateTime? CreationTime { get; protected set; }

        public virtual void SyncAuditState(int? creatorUserId = default, DateTime? lastModificationTime = default, int? lastmodifierUserId = default, DateTime? creationTime = default)
        {
            this.CreatorUserId = creatorUserId;
            this.LastModificationTime = lastModificationTime;
            this.LastModifierUserId = lastmodifierUserId;
            this.CreationTime = creationTime;
        }
    }
}
