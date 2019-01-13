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
    public class CrudAppService<TEntity, TEntityDto, TKey> : QueryAppServiceAsync<TEntity, TEntityDto, TKey>, ICrudAppService, IApplicationService 
         where TEntity : class, IEntity<TKey>, IAggregateRoot, IObjectState
    {
        public CrudAppService(IRepositoryAsync<TEntity, TKey> repositoryAsync
            , IObjectMapper objectMapper) : base(repositoryAsync, objectMapper)
        {

        }
        
        public virtual TEntityDto Add(Func<TEntity> when)
        {
            var aggregate = when();
            var insertedAggregate = _repositoryAsync.Insert(aggregate);

            return base._objectMapper.MapTo<TEntityDto>(
               insertedAggregate
             );
        }

        public virtual TEntityDto Update(TKey id, Action<TEntity> when)
        {
            TEntity aggregate =  base._repositoryAsync.Find(id);
            when(aggregate);
            aggregate =_repositoryAsync.Update(aggregate);
            
            return base._objectMapper.MapTo<TEntityDto>(
             aggregate
             );
        }

        public virtual void Delete(TKey id)
        {
            _repositoryAsync.Delete(id);
        }
    }
    
}

    


