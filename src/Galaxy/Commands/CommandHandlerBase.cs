using Galaxy.Domain;
using Galaxy.Infrastructure;
using Galaxy.Repositories;
using Galaxy.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Commands
{
   public abstract class CommandHandlerBase<TAggregateRoot, TPrimaryKey> where TAggregateRoot : class, IAggregateRoot, IObjectState
    {
        protected readonly IUnitOfWorkAsync _unitOfWorkAsync;
        protected readonly IRepositoryAsync<TAggregateRoot, TPrimaryKey> _aggregateRootRepository;
        public CommandHandlerBase(IUnitOfWorkAsync unitOfWorkAsync
            , IRepositoryAsync<TAggregateRoot, TPrimaryKey> aggregateRootRepository)
        {
            _unitOfWorkAsync = unitOfWorkAsync ?? throw new ArgumentNullException(nameof(unitOfWorkAsync));
            _aggregateRootRepository = aggregateRootRepository ?? throw new ArgumentNullException(nameof(aggregateRootRepository));
        }
        
        public virtual async Task AddAsync(Func<IRepositoryAsync<TAggregateRoot, TPrimaryKey>, Task> when)
        {
            await when(_aggregateRootRepository);
            await this._unitOfWorkAsync.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TPrimaryKey id, Func<TAggregateRoot, Task> when)
        {
            TAggregateRoot aggregate = _aggregateRootRepository.Find(id);
            await when(aggregate);
            await this._unitOfWorkAsync.SaveChangesAsync();
        }

        public virtual void Add(Action<IRepositoryAsync<TAggregateRoot, TPrimaryKey>> when)
        {
            when(_aggregateRootRepository);

            this._unitOfWorkAsync.SaveChangesAsync()
                .ConfigureAwait(false)
                .GetAwaiter().GetResult();
        }

        public virtual void Update(TPrimaryKey id, Action<TAggregateRoot> when)
        {
            TAggregateRoot aggregate = _aggregateRootRepository.Find(id);
            when(aggregate);
            this._unitOfWorkAsync.SaveChangesAsync()
                 .ConfigureAwait(false)
                 .GetAwaiter().GetResult();

        }

    }
}
