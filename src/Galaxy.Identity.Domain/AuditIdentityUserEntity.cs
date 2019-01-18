using Galaxy.Domain;
using Galaxy.Domain.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Identity.Domain
{
    public class AuditIdentityUserEntity : AuditIdentityUserEntity<int>, IAggregateRoot, IEntity, IAudit
    {
    }
}
