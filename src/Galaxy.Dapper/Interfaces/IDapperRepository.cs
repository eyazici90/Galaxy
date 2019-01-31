using Galaxy.Configurations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Galaxy.Dapper.Interfaces
{
    public interface IDapperRepository : IMarkerInterface
    {
        #region Anonymous Prototypes

        IEnumerable<object> Query(string sql, int? timeout = null);

        IEnumerable<object> Query(string sql, object param, int? timeout = null);

        Task<IEnumerable<object>> QueryAsync(string sql, int? timeout = null);

        Task<IEnumerable<object>> QueryAsync(string sql, object param, int? timeout = null);

        IEnumerable<object> QueryViaStoredProc(string proc_name, int? timeout = null);

        IEnumerable<object> QueryViaStoredProc(string proc_name, object param, int? timeout = null);

        Task<IEnumerable<object>> QueryViaStoredProcAsync(string proc_name, int? timeout = null);

        Task<IEnumerable<object>> QueryViaStoredProcAsync(string proc_name, object param, int? timeout = null);

        #endregion

        #region Strongly Typed Prototypes 

        IEnumerable<T> Query<T>(string sql, int? timeout = null);

        IEnumerable<T> Query<T>(string sql, object param, int? timeout = null);

        Task<IEnumerable<T>> QueryAsync<T>(string sql, int? timeout = null);

        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param, int? timeout = null);

        IEnumerable<T> QueryViaStoredProc<T>(string proc_name, int? timeout = null);

        IEnumerable<T> QueryViaStoredProc<T>(string proc_name, object param, int? timeout = null);

        Task<IEnumerable<T>> QueryViaStoredProcAsync<T>(string proc_name, int? timeout = null);

        Task<IEnumerable<T>> QueryViaStoredProcAsync<T>(string proc_name, object param, int? timeout);

        #endregion
    }
}
