using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Domain
{ 
    public abstract class AggregateRootEntityState : AggregateRootEntityState<int>, IAggregateRoot, IEntity
    {
    }
}
