using Dapper;
using Galaxy.DataContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Galaxy.Dapper
{
    public class DapperRepository : DapperRepository<object>, IDapperRepository
    {
        public DapperRepository(IDapperSettings dapperSettings
            , IActiveDbConnectionProvider activeConnectionProvider
            , IActiveDbTransactionProvider activeTransactionProvider) : base(dapperSettings, activeConnectionProvider, activeTransactionProvider)
        {
        }
    }
}
