using Galaxy.Domain;
using Galaxy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Application
{
    public interface IQueryAppServiceAsync<TEntityDto, in TKey, TEntity> : IQueryAppService<TEntityDto,TKey,TEntity>
         where TEntityDto : IEntityDto<TKey>
         where TEntity : class, IEntity<TKey>, IAggregateRoot, IObjectState
    {
        Task<TEntityDto> FindAsync(TKey id);

        Task<TEntityDto> GetAsync(TKey id);
    }
}
