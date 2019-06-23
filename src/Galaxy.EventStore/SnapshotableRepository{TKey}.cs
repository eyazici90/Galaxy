using Galaxy.Domain;
using Galaxy.Infrastructure;
using Galaxy.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Galaxy.EventStore
{
    public class SnapshotableRepository<TAggregateRoot, TKey> : IRepositoryAsync<TAggregateRoot, TKey>, IRepository<TAggregateRoot, TKey>
        where TAggregateRoot : class, IAggregateRoot, IObjectState
    {
        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public void Delete(TAggregateRoot entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TAggregateRoot> ExecuteQuery(string sqlQuery)
        {
            throw new NotImplementedException();
        }

        public TAggregateRoot Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public Task<TAggregateRoot> FindAsync(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public Task<TAggregateRoot> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public TAggregateRoot Insert(TAggregateRoot entity)
        {
            throw new NotImplementedException();
        }

        public Task<TAggregateRoot> InsertAsync(TAggregateRoot entity)
        {
            throw new NotImplementedException();
        }

        public void InsertGraphRange(IEnumerable<TAggregateRoot> entities)
        {
            throw new NotImplementedException();
        }

        public void InsertOrUpdateGraph(TAggregateRoot entity)
        {
            throw new NotImplementedException();
        }

        public void InsertRange(IEnumerable<TAggregateRoot> entities)
        {
            throw new NotImplementedException();
        }

        public IQueryFluent<TAggregateRoot> Query(IQueryObject<TAggregateRoot> queryObject)
        {
            throw new NotImplementedException();
        }

        public IQueryFluent<TAggregateRoot> Query(Expression<Func<TAggregateRoot, bool>> query)
        {
            throw new NotImplementedException();
        }

        public IQueryFluent<TAggregateRoot> Query()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TAggregateRoot> Queryable()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TAggregateRoot> QueryableNoTrack()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TAggregateRoot> QueryableWithNoFilter()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TAggregateRoot> SelectQuery(string query, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public TAggregateRoot Update(TAggregateRoot entity)
        {
            throw new NotImplementedException();
        }

        public Task<TAggregateRoot> UpdateAsync(TAggregateRoot entity)
        {
            throw new NotImplementedException();
        }
         
    }
}
