using Galaxy.Domain;
using Galaxy.Domain.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Auditing
{
    public interface IFullyAudit<TPrimaryKey> : IMultiTenant, IAudit, ISoftDelete
    { 
        
    }
}
