using Galaxy.Domain;
using Galaxy.Infrastructure;
using Galaxy.ObjectMapping;
using Galaxy.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Application
{
    public abstract class QueryAppServiceAsync<TEntityDto, TKey, TEntity> : QueryAppService<TEntityDto, TKey, TEntity>, IQueryAppServiceAsync<TEntityDto, TKey, TEntity>
        // where TEntityDto : IEntityDto<TKey>
         where TEntity : class, IEntity<TKey>, IAggregateRoot, IObjectState
    {
        public QueryAppServiceAsync(IRepositoryAsync<TEntity, TKey> repositoryAsync, IObjectMapper objectMapper) : base(repositoryAsync, objectMapper)
        {
        }
        
        public virtual async Task<TEntityDto> FindAsync(TKey id) =>
                this._objectMapper.MapTo<TEntityDto>(
                    await base._repositoryAsync.FindAsync(id)
                );

        public virtual async Task<TEntityDto> GetAsync(TKey id) =>
                this._objectMapper.MapTo<TEntityDto>(
                         await base._repositoryAsync.FindAsync(id)
                     );
        
    }
}
