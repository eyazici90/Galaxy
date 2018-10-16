using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Domain
{
    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey Id { get;  }

        bool IsTransient();
    }
}
