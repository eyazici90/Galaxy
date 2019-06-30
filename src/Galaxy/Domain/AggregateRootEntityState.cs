using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Domain
{ 
    public abstract class AggregateRootEntityState<TState> : AggregateRootEntityState<TState, int>, IAggregateRoot, IEntity
           where TState : class
    {
    }
}
