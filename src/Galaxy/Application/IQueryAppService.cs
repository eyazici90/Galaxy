using Galaxy.Domain;
using Galaxy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Galaxy.Application
{
    public interface IQueryAppService<TEntity, TEntityDto, TKey>
        : IApplicationService 
           where TEntity : class, IEntity<TKey>, IAggregateRoot, IObjectState
    {

        IQueryable<TEntityDto> Queryable();

        IQueryable<TEntityDto> QueryableNoTrack();

        IQueryable<TEntityDto> QueryableWithNoFilter();

        TEntityDto Find(TKey id);

        TEntityDto Get(TKey id);

        IList<TEntityDto> GetAll();

        IQueryable<TEntityDto> GetAll(Expression<Func<TEntity, bool>> whereCondition = default);
    }
}
