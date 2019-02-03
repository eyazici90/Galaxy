using Galaxy.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Galaxy.EntityFrameworkCore
{
    public class ActiveDbConnectionProvider : IActiveDbConnectionProvider, IActiveDbTransactionProvider
    {
        private readonly IGalaxyContextAsync _context;
        public ActiveDbConnectionProvider(IGalaxyContextAsync context)
        {
            _context = context;
        }

        public IDbConnection GetActiveDbConnection()
        {
           return ((DbContext) _context)?.Database.GetDbConnection();  
        }

        public IDbTransaction GetActiveDbTransaction()
        {
            return ((DbContext)_context).Database.CurrentTransaction?.GetDbTransaction();
        }
    }
}
