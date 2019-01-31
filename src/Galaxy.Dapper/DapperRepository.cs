using Dapper;
using Galaxy.Dapper.Interfaces;
using Galaxy.DataContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Galaxy.Dapper
{
    public class DapperRepository : IDapperRepository
    {
        private readonly DapperGlobalSettings _dapperSettings;
        private readonly IActiveDbConnectionProvider _activeConnectionProvider;

        private IDbConnection _dbConnection;
        private IDbConnection DbConnection
        { 
            get
            {
                if (_dbConnection == null)
                {
                    _dbConnection = _activeConnectionProvider.GetActiveDbConnection();
                    if (_dbConnection.State == ConnectionState.Open)
                    {
                        _dbConnection.Close();
                    }
                    _dbConnection.Open();
                }
                return _dbConnection;
            }
        }

        public DapperRepository(DapperGlobalSettings dapperSettings
                               , IActiveDbConnectionProvider activeConnectionProvider)
        {
            _activeConnectionProvider = activeConnectionProvider ?? throw new ArgumentNullException(nameof(activeConnectionProvider));
            _dapperSettings = dapperSettings ?? throw new ArgumentNullException(nameof(dapperSettings));
        }

        private int SetCommandTimout(int? timeout)
        {
            return timeout.HasValue ? timeout.Value : _dapperSettings.CommandTimeout;
        }

        public IEnumerable<object> Query(string sql, int? timeout = null)
        {
            IEnumerable<object> result = this.DbConnection.Query<object>(sql, commandTimeout: SetCommandTimout(timeout));
            return result;
        }

        public IEnumerable<object> Query(string sql, object param, int? timeout = null)
        {
            IEnumerable<object> result = this.DbConnection.Query<object>(sql, param, commandTimeout: SetCommandTimout(timeout));
            return result;
        }

        public IEnumerable<T> Query<T>(string sql, int? timeout = null)
        {
            IEnumerable<T> result = this.DbConnection.Query<T>(sql, commandTimeout: SetCommandTimout(timeout));
            return result;
        }

        public IEnumerable<T> Query<T>(string sql, object param, int? timeout = null)
        {
            IEnumerable<T> result = this.DbConnection.Query<T>(sql, param, commandTimeout: SetCommandTimout(timeout));
            return result;
        }

        public Task<IEnumerable<object>> QueryAsync(string sql, int? timeout = null)
        {
            return this.DbConnection.QueryAsync<object>(sql, commandTimeout: SetCommandTimout(timeout));
        }

        public Task<IEnumerable<object>> QueryAsync(string sql, object param, int? timeout = null)
        {
            return this.DbConnection.QueryAsync<object>(sql, param, commandTimeout: SetCommandTimout(timeout));
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, int? timeout = null)
        {
            return this.DbConnection.QueryAsync<T>(sql, commandTimeout: SetCommandTimout(timeout));
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param, int? timeout = null)
        {
            return this.DbConnection.QueryAsync<T>(sql, param, commandTimeout: SetCommandTimout(timeout));
        }

        public IEnumerable<object> QueryViaStoredProc(string proc_name, int? timeout = null)
        {
            IEnumerable<object> result = this.DbConnection.Query<object>(proc_name, commandTimeout: SetCommandTimout(timeout), commandType: CommandType.StoredProcedure);
            return result;
        }

        public IEnumerable<object> QueryViaStoredProc(string proc_name, object param, int? timeout = null)
        {
            IEnumerable<object> result = this.DbConnection.Query<object>(proc_name, param, commandTimeout: SetCommandTimout(timeout), commandType: CommandType.StoredProcedure);
            return result;
        }

        public IEnumerable<T> QueryViaStoredProc<T>(string proc_name, int? timeout = null)
        {
            IEnumerable<T> result = this.DbConnection.Query<T>(proc_name, commandTimeout: SetCommandTimout(timeout), commandType: CommandType.StoredProcedure);
            return result;
        }

        public IEnumerable<T> QueryViaStoredProc<T>(string proc_name, object param, int? timeout = null)
        {
            IEnumerable<T> result = this.DbConnection.Query<T>(proc_name, param, commandTimeout: SetCommandTimout(timeout), commandType: CommandType.StoredProcedure);
            return result;
        }

        public Task<IEnumerable<object>> QueryViaStoredProcAsync(string proc_name, int? timeout = null)
        {
            return this.DbConnection.QueryAsync<object>(proc_name, commandTimeout: SetCommandTimout(timeout), commandType: CommandType.StoredProcedure);
        }

        public Task<IEnumerable<object>> QueryViaStoredProcAsync(string proc_name, object param, int? timeout = null)
        {
            return this.DbConnection.QueryAsync<object>(proc_name, param, commandTimeout: SetCommandTimout(timeout), commandType: CommandType.StoredProcedure);
        }

        public Task<IEnumerable<T>> QueryViaStoredProcAsync<T>(string proc_name, int? timeout = null)
        {
            return this.DbConnection.QueryAsync<T>(proc_name, commandTimeout: SetCommandTimout(timeout), commandType: CommandType.StoredProcedure);
        }

        public Task<IEnumerable<T>> QueryViaStoredProcAsync<T>(string proc_name, object param, int? timeout)
        {
            return this.DbConnection.QueryAsync<T>(proc_name, param, commandTimeout: SetCommandTimout(timeout), commandType: CommandType.StoredProcedure);
        }
    }
}
