using Galaxy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Domain
{ 
    public abstract class EntityState : EntityState<int>, IEntity, IObjectState
    {
    }
}
