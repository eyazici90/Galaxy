using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Domain
{
    public abstract class AggregateRootEntity : AggregateRootEntity<int> , IAggregateRoot, IEntity
    {
    }
}
