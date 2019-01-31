using System.Data;

namespace Galaxy.DataContext
{
    public interface IActiveDbConnectionProvider
    {
        IDbConnection GetActiveDbConnection();
    }
}
