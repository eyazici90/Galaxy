using Galaxy.Domain;
using Galaxy.Domain.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Auditing
{
    public abstract class FullyAuditEntity<TPrimaryKey> : AuditEntity<TPrimaryKey>, IEntity<TPrimaryKey>, IFullyAudit
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
