using Galaxy.DataContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Dapper
{
    public class DapperRepository<TEntity> : DapperRepository<TEntity, int>, IDapperRepository<TEntity> where TEntity : class
    {
        public DapperRepository(IDapperSettings dapperSettings
                                 , IActiveDbConnectionProvider activeConnectionProvider
                                 , IActiveDbTransactionProvider activeTransactionProvider) : base(dapperSettings, activeConnectionProvider, activeTransactionProvider)
        {
        }
    }
}
