using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Galaxy.DataContext
{
    public interface IActiveDbTransactionProvider
    {
        IDbTransaction GetActiveDbTransaction();
    }
}
