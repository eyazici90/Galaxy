using Galaxy.Domain;
using Galaxy.Infrastructure;
using Galaxy.Repositories;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Galaxy.UnitOfWork
{
    public interface IUnitOfWorkAsync : IUnitOfWork
    {
        Task BeginTransactionAsync(IUnitOfWorkOptions unitOfWorkOptions = default);
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class, IAggregateRoot, IObjectState;
        Task<bool> CommitAsync();
        Task<int> SaveChangesByPassAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}