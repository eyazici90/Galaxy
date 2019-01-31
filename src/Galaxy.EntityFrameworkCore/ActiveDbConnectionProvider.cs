using Galaxy.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Galaxy.EntityFrameworkCore
{
    public class ActiveDbConnectionProvider : IActiveDbConnectionProvider
    {
        private readonly IGalaxyContextAsync _context;
        public ActiveDbConnectionProvider(IGalaxyContextAsync context)
        {
            _context = context;
        }

        public IDbConnection GetActiveDbConnection()
        {
            var connection = ((DbContext) _context).Database.GetDbConnection();  
            return connection;
        } 
    }
}
