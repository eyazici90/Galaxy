using Galaxy.Auditing;
using Galaxy.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Identity
{
    public abstract class FullyAuditIdentityRoleEntity : FullyAuditIdentityRoleEntity<int>, IAggregateRoot, IEntity, IFullyAudit
    {
    }
}
