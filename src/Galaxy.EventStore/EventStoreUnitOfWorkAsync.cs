using EventStore.ClientAPI;
using EventStore.ClientAPI.Exceptions;
using Galaxy.Domain;
using Galaxy.Exceptions;
using Galaxy.Infrastructure;
using Galaxy.Repositories;
using Galaxy.Serialization;
using Galaxy.UnitOfWork;
using MediatR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Galaxy.EventStore
{
    public class EventStoreUnitOfWorkAsync : IUnitOfWorkAsync
    {
        private bool _disposed;
        private ConcurrentDictionary<string, Aggregate> _aggregates;
        private readonly IEventStoreConnection _connection;
        private readonly ISerializer _serializer;
        private readonly IMediator _mediatR;
        public EventStoreUnitOfWorkAsync(IEventStoreConnection connection
            , IGalaxyEventStoreConfigurations eventStoreConfigurations
            , ISerializer serializer
            , IMediator mediatR)
        {
            _aggregates = new ConcurrentDictionary<string, Aggregate>();
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            _mediatR = mediatR ?? throw new ArgumentNullException(nameof(mediatR));
        }

        public async Task DispatchNotificationsAsync(IMediator mediator)
        {
            var notifications = this._aggregates.Values.Select(a=>(a.Root as IEntity)) ;

            var domainEvents = notifications
                .SelectMany(x => x.DomainEvents)
                .ToList();

            notifications.ToList()
                .ForEach(entity => entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await mediator.Publish(domainEvent);
                });

            await Task.WhenAll(tasks);
        }

        private async Task<int> SendToStreamAsync()
        {
            int eventCount = 0;
            foreach (Aggregate aggregate in _aggregates.Values)
            {
                EventData[] changes = (aggregate.Root as IEntity).DomainEvents
                                               .Select(@event => new EventData(
                                                   Guid.NewGuid(),
                                                   @event.GetType().Name,
                                                   true,
                                                   Encoding.UTF8.GetBytes(this._serializer.Serialize(@event)),
                                                   Encoding.UTF8.GetBytes(this._serializer.Serialize(new EventMetadata
                                                   {
                                                       TimeStamp = DateTime.Now,
                                                       AggregateType = aggregate.GetType().Name,
                                                       AggregateAssemblyQualifiedName = aggregate.GetType().AssemblyQualifiedName,
                                                       IsSnapshot = false
                                                   }))
                                                   )).ToArray();
                try
                {
                    await this._connection.AppendToStreamAsync(StreamExtensions.GetStreamName(aggregate), aggregate.Version, changes);
                   
                    eventCount = eventCount + changes.Length;
                }
                catch (WrongExpectedVersionException ex)
                {
                    throw ex;
                }
            }
            return eventCount;
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            
        }

        public bool CheckIfThereIsAvailableTransaction() => _aggregates.Values.Any();
       

        public bool Commit()
        {
            SendToStreamAsync().ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
            DispatchNotificationsAsync(this._mediatR).ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
            return true;
        }

        public async Task<bool> CommitAsync()
        {
            await SendToStreamAsync();
            await DispatchNotificationsAsync(this._mediatR);
            return await Task.FromResult(true);
        }
        
        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            var result = SendToStreamAsync().ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
            DispatchNotificationsAsync(this._mediatR).ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
            return result;
        }

        public async Task<int> SaveChangesAsync()
        {
            var result = await SendToStreamAsync();
            await DispatchNotificationsAsync(this._mediatR);
            return result;
        }

        public async  Task<int> SaveChangesAsync(CancellationToken cancellationToken) =>
             await this.SaveChangesAsync();
        

        public async Task<int> SaveChangesByPassAsync(CancellationToken cancellationToken = default(CancellationToken)) =>
            await this.SaveChangesAsync();

        public IRepository<TEntity> Repository<TEntity>()  where TEntity : class, IAggregateRoot, IObjectState
        {
            throw new NotImplementedException();
        }

        public IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class, IAggregateRoot, IObjectState
        {
            throw new NotImplementedException();
        }
  
        public void Attach(object aggregate)
        {
            if (aggregate == null)
            {
                throw new ArgumentNullException(nameof(aggregate));
            }
          
            var castedAggregateRoot = aggregate as Aggregate;
            if (!_aggregates.TryAdd(castedAggregateRoot.Identifier, castedAggregateRoot))
            {
                throw new GalaxyException($"The same aggregate getting attached multiple times.");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (this._aggregates != null)
                {
                    _aggregates.Clear();
                    _aggregates = null;
                }
            }
            _disposed = true;
        }


    }
}
