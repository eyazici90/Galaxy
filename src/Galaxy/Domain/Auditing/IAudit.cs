using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Domain.Auditing
{
    public interface IAudit
    {
        int? CreatorUserId { get; }
        DateTime? LastModificationTime { get; }
        int? LastModifierUserId { get; }
        DateTime? CreationTime { get; }

        void SyncAuditState(int? creatorUserId = default, DateTime? lastModificationTime = default
            , int? lastmodifierUserId = default, DateTime? creationTime = default);
    }
}
