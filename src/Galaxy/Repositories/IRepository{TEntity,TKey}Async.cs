using Galaxy.Domain;
using Galaxy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Galaxy.Repositories
{
    public interface IRepositoryAsync<TEntity, TPrimaryKey> : IRepositoryAsync, IRepository<TEntity,TPrimaryKey> where TEntity : class, IAggregateRoot, IObjectState
    {
        Task InsertAsync(TEntity entity);
        Task<TEntity> FindAsync(params object[] keyValues);
        Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);
        Task<bool> DeleteAsync(params object[] keyValues);
        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);
    }
}
