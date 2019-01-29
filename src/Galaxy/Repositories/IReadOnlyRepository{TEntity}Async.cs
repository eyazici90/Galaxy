using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Galaxy.Repositories
{
    public interface IReadOnlyRepositoryAsync<TEntity>  : IReadOnlyRepository<TEntity> where TEntity : class
    {
        Task<TEntity> FindAsync(params object[] keyValues);
        Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);
    }
}
