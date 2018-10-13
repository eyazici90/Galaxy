
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Galaxy.DataContext
{
    public interface IGalaxyContextAsync : IGalaxyContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();

        Task<int> SaveChangesByPassedAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveChangesByPassedAsync();

        Task DispatchNotificationsAsync(IMediator mediator);
    }
}
