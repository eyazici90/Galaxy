using Galaxy.Domain;
using Galaxy.Infrastructure;
using Galaxy.ObjectMapping;
using Galaxy.Repositories;
using Galaxy.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Application
{
    public class CrudAppService<TEntityDto, TKey, TEntity> : QueryAppServiceAsync<TEntityDto, TKey, TEntity>, ICrudAppService, IApplicationService
        // where TEntityDto : IEntityDto<TKey>
         where TEntity : class, IEntity<TKey>, IAggregateRoot, IObjectState
    {
        protected readonly IUnitOfWorkAsync _unitOfWorkAsync;
        public CrudAppService(IRepositoryAsync<TEntity, TKey> repositoryAsync
            , IObjectMapper objectMapper
            , IUnitOfWorkAsync unitOfWorkAsync) : base(repositoryAsync, objectMapper)
        {
            this._unitOfWorkAsync = unitOfWorkAsync;
        }

   
        public virtual TEntityDto Add(Func<TEntity> when)
        {
            var aggregate = when();
            _repositoryAsync.Insert(aggregate);
            this._unitOfWorkAsync.SaveChangesAsync()
                .ConfigureAwait(false)
                .GetAwaiter().GetResult();

            return base._objectMapper.MapTo<TEntityDto>(
               aggregate
             );
        }

        public virtual TEntityDto Update(TKey id, Action<TEntity> when)
        {
            TEntity aggregate =  base._repositoryAsync.Find(id);
            when(aggregate);
            _repositoryAsync.Update(aggregate);
            this._unitOfWorkAsync.SaveChangesAsync()
                 .ConfigureAwait(false)
                 .GetAwaiter().GetResult();

            return base._objectMapper.MapTo<TEntityDto>(
             aggregate
             );
        }

        public virtual void Delete(TKey id)
        {
            _repositoryAsync.Delete(id);
            this._unitOfWorkAsync.SaveChangesAsync()
                 .ConfigureAwait(false)
                 .GetAwaiter().GetResult();
        }


    }
}
