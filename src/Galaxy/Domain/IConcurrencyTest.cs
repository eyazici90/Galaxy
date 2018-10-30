using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Domain
{
    public interface IConcurrencyTest
    {
         byte[] RowVersion { get;  }
    }
}
