using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galaxy.Repositories
{
    public interface IReadOnlyRepository<TEntity> where TEntity : class
    {
        TEntity Find(params object[] keyValues);
        IQueryable<TEntity> Queryable();
        IQueryable<TEntity> QueryableNoTrack();
        IQueryable<TEntity> QueryableWithNoFilter();
        IQueryable<TEntity> SelectQuery(string query, params object[] parameters);
    }
}
