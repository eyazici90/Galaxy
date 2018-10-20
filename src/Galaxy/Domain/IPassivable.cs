using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Domain
{
   public interface IPassivable
    {
        bool IsActive { get;  }
    }
}
