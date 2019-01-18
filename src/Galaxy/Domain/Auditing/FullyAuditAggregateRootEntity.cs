using Galaxy.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Domain.Auditing
{
    
    public abstract class FullyAuditAggregateRootEntity : FullyAuditAggregateRootEntity<int>, IAggregateRoot, IEntity , IFullyAudit
    {
    }
}
