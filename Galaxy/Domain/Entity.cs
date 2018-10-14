
using Galaxy.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Domain
{
    public abstract class Entity : Entity<int>, IObjectState , IEntity<int>, IEntity
    {
        
    }
}
