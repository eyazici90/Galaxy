using Dapper;
using Galaxy.DataContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Dapper
{
    public class DapperRepository<TEntity, TKey> : IDapperRepository<TEntity, TKey>  where TEntity : class
    {
        protected readonly IDapperSettings DapperSettings;

        protected readonly IActiveDbConnectionProvider ActiveConnectionProvider;

        protected readonly IActiveDbTransactionProvider ActiveTransactionProvider;

        protected IDbConnection DbConnection => ActiveConnectionProvider.GetActiveDbConnection();

        protected IDbTransaction DbTransaction => ActiveTransactionProvider.GetActiveDbTransaction();

        public DapperRepository(IDapperSettings dapperSettings
                               , IActiveDbConnectionProvider activeConnectionProvider
                               , IActiveDbTransactionProvider activeTransactionProvider)
        {
            ActiveConnectionProvider = activeConnectionProvider ?? throw new ArgumentNullException(nameof(activeConnectionProvider));

            ActiveTransactionProvider = activeTransactionProvider ?? throw new ArgumentNullException(nameof(activeTransactionProvider));

            DapperSettings = dapperSettings ?? throw new ArgumentNullException(nameof(dapperSettings));
        }

        public IEnumerable<TEntity> Query(string sql, int? timeout = null)
        { 
            return DbConnection.Query<TEntity>(sql, commandTimeout: SetGlobalCommandTimoutIfNotDefined(timeout), transaction: DbTransaction);
        }

        public IEnumerable<TEntity> Query(string sql, object param, int? timeout = null)
        {
            return DbConnection.Query<TEntity>(sql, param, commandTimeout: SetGlobalCommandTimoutIfNotDefined(timeout), transaction: DbTransaction);
        }

        public Task<IEnumerable<TEntity>> QueryAsync(string sql, int? timeout = null)
        {
            return DbConnection.QueryAsync<TEntity>(sql, commandTimeout: SetGlobalCommandTimoutIfNotDefined(timeout), transaction: DbTransaction);
        }

        public Task<IEnumerable<TEntity>> QueryAsync(string sql, object param, int? timeout = null)
        {
            return DbConnection.QueryAsync<TEntity>(sql, param, commandTimeout: SetGlobalCommandTimoutIfNotDefined(timeout), transaction: DbTransaction);
        }

        public IEnumerable<TEntity> QueryViaStoredProc(string proc_name, int? timeout = null)
        {
            return DbConnection.Query<TEntity>(proc_name, commandTimeout: SetGlobalCommandTimoutIfNotDefined(timeout)
                , commandType: CommandType.StoredProcedure, transaction: DbTransaction);
        }

        public IEnumerable<TEntity> QueryViaStoredProc(string proc_name, object param, int? timeout = null)
        {
            return DbConnection.Query<TEntity>(proc_name, param, commandTimeout: SetGlobalCommandTimoutIfNotDefined(timeout)
                , commandType: CommandType.StoredProcedure, transaction: DbTransaction);
        }

        public Task<IEnumerable<TEntity>> QueryViaStoredProcAsync(string proc_name, int? timeout = null)
        {
            return DbConnection.QueryAsync<TEntity>(proc_name, commandTimeout: SetGlobalCommandTimoutIfNotDefined(timeout)
                , commandType: CommandType.StoredProcedure, transaction: DbTransaction);
        }

        public Task<IEnumerable<TEntity>> QueryViaStoredProcAsync(string proc_name, object param, int? timeout)
        {
            return DbConnection.QueryAsync<TEntity>(proc_name, param, commandTimeout: SetGlobalCommandTimoutIfNotDefined(timeout)
                , commandType: CommandType.StoredProcedure, transaction: DbTransaction);
        }

        private int SetGlobalCommandTimoutIfNotDefined(int? timeout) =>
            timeout.HasValue ? timeout.Value : DapperSettings.CommandTimeout;

        private void EnsureDbConnectionWithOpenState(IDbConnection dbConnection)
        {
            if (dbConnection.State == ConnectionState.Closed)
            {
                DbConnection.Open();
            } 
        }
    }
}
