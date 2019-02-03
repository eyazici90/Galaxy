using Galaxy.DataContext;
using Galaxy.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Galaxy.Dapper
{
    public class NullDbConnectionProvider : IActiveDbConnectionProvider, IActiveDbTransactionProvider
    {
        public IDbConnection GetActiveDbConnection()
        {
            throw new GalaxyException($"You should register {nameof(IActiveDbConnectionProvider)} for using GalaxyDapper without using Galaxy.EntityFrameworkCore!!!");
        }

        public IDbTransaction GetActiveDbTransaction()
        {
            throw new GalaxyException($"You should register {nameof(IActiveDbTransactionProvider)} for using GalaxyDapper without using Galaxy.EntityFrameworkCore!!!");
        }
    }
}
