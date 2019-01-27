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
   public abstract class CommandHandlerBase<TAggregateRoot, TPrimaryKey> : ICommandHandler where TAggregateRoot : class, IAggregateRoot, IObjectState
    {
        protected readonly IUnitOfWorkAsync UnitOfWorkAsync;
        protected readonly IRepositoryAsync<TAggregateRoot, TPrimaryKey> AggregateRootRepository;
        public CommandHandlerBase(IUnitOfWorkAsync unitOfWorkAsync
            , IRepositoryAsync<TAggregateRoot, TPrimaryKey> aggregateRootRepository)
        {
            UnitOfWorkAsync = unitOfWorkAsync ?? throw new ArgumentNullException(nameof(unitOfWorkAsync));
            AggregateRootRepository = aggregateRootRepository ?? throw new ArgumentNullException(nameof(aggregateRootRepository));
        }

        public virtual async Task AddAsync(Func<Task<TAggregateRoot>> when)
        {
            var aggregate = await when();
            await AggregateRootRepository.InsertAsync(aggregate);
            await this.UnitOfWorkAsync.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TPrimaryKey id, Func<TAggregateRoot, Task> when)
        {
            TAggregateRoot aggregate = await AggregateRootRepository.FindAsync(id);
            await when(aggregate);
            await AggregateRootRepository.UpdateAsync(aggregate);
            await this.UnitOfWorkAsync.SaveChangesAsync();
        }


        public virtual void Add(Func<TAggregateRoot> when)
        {
            var aggregate = when();
            AggregateRootRepository.Insert(aggregate);
            this.UnitOfWorkAsync.SaveChangesAsync()
                .ConfigureAwait(false)
                .GetAwaiter().GetResult();
        }

        public virtual void Update(TPrimaryKey id, Action<TAggregateRoot> when)
        {
            TAggregateRoot aggregate = AggregateRootRepository.Find(id);
            when(aggregate);
            AggregateRootRepository.Update(aggregate);
            this.UnitOfWorkAsync.SaveChangesAsync()
                 .ConfigureAwait(false)
                 .GetAwaiter().GetResult();
        }

    }
}
