using Galaxy.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Galaxy.Domain
{
    public abstract class AggregateRootEntity<TPrimaryKey> : Entity<TPrimaryKey>, IAggregateRoot, IObjectState, IEntity<TPrimaryKey>
    {
    
    }
}

