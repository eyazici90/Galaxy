using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Domain
{
    public interface IEntity<TPrimaryKey> : IEntity
    {
        TPrimaryKey Id { get;  }
        bool IsTransient();

    }
}
