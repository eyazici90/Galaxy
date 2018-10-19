
using Galaxy.Domain;
using Galaxy.Domain.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Auditing
{
    public abstract class FullyAuditEntity : FullyAuditEntity<int>, IFullyAudit, ISoftDelete
    {
      
    }
}
