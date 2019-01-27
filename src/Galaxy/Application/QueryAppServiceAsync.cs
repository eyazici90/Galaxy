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
    public abstract class QueryAppServiceAsync<TEntity, TEntityDto, TKey> : QueryAppService<TEntity, TEntityDto, TKey>, IQueryAppServiceAsync<TEntity, TEntityDto, TKey>
         where TEntity : class, IEntity<TKey>, IAggregateRoot, IObjectState
    {
        public QueryAppServiceAsync(IRepositoryAsync<TEntity, TKey> repositoryAsync, IObjectMapper objectMapper) : base(repositoryAsync, objectMapper)
        {
        }
        
        public virtual async Task<TEntityDto> FindAsync(TKey id) =>
                this.ObjectMapper.MapTo<TEntityDto>(
                    await base.RepositoryAsync.FindAsync(id)
                );

        public virtual async Task<TEntityDto> GetAsync(TKey id) =>
                this.ObjectMapper.MapTo<TEntityDto>(
                         await base.RepositoryAsync.FindAsync(id)
                     );
        
    }
}
