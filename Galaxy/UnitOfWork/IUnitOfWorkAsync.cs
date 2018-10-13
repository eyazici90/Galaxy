using Galaxy.Domain;
using Galaxy.Infrastructure;
using Galaxy.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Galaxy.UnitOfWork
{
    public interface IUnitOfWorkAsync : IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class, IAggregateRoot, IObjectState;

        Task<int> SaveChangesByPassAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}