using Galaxy.Domain;
using Galaxy.Infrastructure;
using Galaxy.ObjectMapping;
using Galaxy.Repositories;
using Galaxy.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Commands
{
   public abstract class CommandHandlerBase<TAggregateRoot, TEntityDto, TPrimaryKey> : ICommandHandler where TAggregateRoot : class, IAggregateRoot, IObjectState
    {
        protected readonly IObjectMapper ObjectMapper;
        protected readonly IUnitOfWorkAsync UnitOfWorkAsync;
        protected readonly IRepositoryAsync<TAggregateRoot, TPrimaryKey> AggregateRootRepository;
        public CommandHandlerBase(IUnitOfWorkAsync unitOfWorkAsync
            , IRepositoryAsync<TAggregateRoot, TPrimaryKey> aggregateRootRepository
            , IObjectMapper objectMapper)
        {
            UnitOfWorkAsync = unitOfWorkAsync ?? throw new ArgumentNullException(nameof(unitOfWorkAsync));
            AggregateRootRepository = aggregateRootRepository ?? throw new ArgumentNullException(nameof(aggregateRootRepository));
            ObjectMapper = objectMapper ?? throw new ArgumentNullException(nameof(objectMapper));
        }

        [DisableUnitOfWork]
        public virtual async Task<TEntityDto> AddAsync(Func<Task<TAggregateRoot>> when)
        {
            var aggregate = await when();

            var insertedAggregate = await AggregateRootRepository.InsertAsync(aggregate);

            await this.UnitOfWorkAsync.SaveChangesAsync();

            return ObjectMapper.MapTo<TEntityDto>(
               insertedAggregate
               );
        }

        [DisableUnitOfWork]
        public virtual async Task<TEntityDto> UpdateAsync(TPrimaryKey id, Func<TAggregateRoot, Task> when)
        {
            TAggregateRoot aggregate = await AggregateRootRepository.FindAsync(id);

            await when(aggregate);

            aggregate = await AggregateRootRepository.UpdateAsync(aggregate);

            await this.UnitOfWorkAsync.SaveChangesAsync();

            return ObjectMapper.MapTo<TEntityDto>(
               aggregate
               );
        }

        [DisableUnitOfWork]
        public virtual async Task DeleteAsync(TPrimaryKey id)
        {
            await AggregateRootRepository.DeleteAsync(id);

            await UnitOfWorkAsync.SaveChangesAsync();
        }

        [DisableUnitOfWork]
        public virtual TEntityDto Add(Func<TAggregateRoot> when)
        {
            var aggregate = when();

            var insertedAggregate = AggregateRootRepository.Insert(aggregate);

            this.UnitOfWorkAsync.SaveChangesAsync()
                .ConfigureAwait(false)
                .GetAwaiter().GetResult();

            return ObjectMapper.MapTo<TEntityDto>(
             insertedAggregate
             );
        }

        [DisableUnitOfWork]
        public virtual TEntityDto Update(TPrimaryKey id, Action<TAggregateRoot> when)
        {
            TAggregateRoot aggregate = AggregateRootRepository.Find(id);

            when(aggregate);

            aggregate = AggregateRootRepository.Update(aggregate);

            this.UnitOfWorkAsync.SaveChangesAsync()
                 .ConfigureAwait(false)
                 .GetAwaiter().GetResult();

            return ObjectMapper.MapTo<TEntityDto>(
             aggregate
             );
        }



    }
}
