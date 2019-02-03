using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Dapper
{ 
    public interface IDapperRepository<TEntity,TKey> where TEntity : class
    {
        IEnumerable<TEntity> Query(string sql, int? timeout = null);

        IEnumerable<TEntity> Query(string sql, object param, int? timeout = null);

        Task<IEnumerable<TEntity>> QueryAsync(string sql, int? timeout = null);

        Task<IEnumerable<TEntity>> QueryAsync(string sql, object param, int? timeout = null);

        IEnumerable<TEntity> QueryViaStoredProc(string procName, int? timeout = null);

        IEnumerable<TEntity> QueryViaStoredProc(string procName, object param, int? timeout = null);

        Task<IEnumerable<TEntity>> QueryViaStoredProcAsync(string procName, int? timeout = null);

        Task<IEnumerable<TEntity>> QueryViaStoredProcAsync(string procName, object param, int? timeout);
    }
}
