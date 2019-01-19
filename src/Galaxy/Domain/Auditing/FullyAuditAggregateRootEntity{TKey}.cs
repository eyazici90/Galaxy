using Galaxy.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Domain.Auditing
{
    public abstract class FullyAuditAggregateRootEntity<TPrimaryKey> : AuditAggregateRootEntity<TPrimaryKey>, IAggregateRoot, IEntity<TPrimaryKey>, IFullyAudit
    {
        public virtual int? TenantId { get; protected set; }

        public virtual bool IsDeleted { get; protected set; }

        public void SyncTenantState(int? tenantId = null)
        {
            if (tenantId.HasValue)
                this.TenantId = tenantId;
        }
    }
}
