using Galaxy.Domain.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Auditing
{
    public interface IFullyAudit<TPrimaryKey> : IMultiTenant
    { 
        int? CreatorUserId { get; }
        DateTime? LastModificationTime { get; }
        int? LastModifierUserId { get; }

        DateTime? CreationTime { get; }
        void SyncAuditState(int? tenantId = default, int? creatorUserId = default, DateTime? lastModificationTime = default
            , int? lastmodifierUserId = default, DateTime? creationTime = default);
    }
}
