using Galaxy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Domain
{
    public abstract class AggregateRootEntityState<TPrimaryKey> : AggregateRootEntity<TPrimaryKey>
                ,IAggregateRoot, IObjectState, IEntity<TPrimaryKey>
    {

    }
}
