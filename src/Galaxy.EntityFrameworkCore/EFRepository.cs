#region
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Galaxy.Repositories;
using Galaxy.Domain;
using Galaxy.Infrastructure;
using Galaxy.DataContext;
using Galaxy.UnitOfWork;

#endregion

namespace Galaxy.EFCore
{

    public  class EFRepository<TEntity> : IRepository<TEntity>, IRepositoryAsync<TEntity> where TEntity : class, IAggregateRoot, IObjectState
    {
        #region Private Fields
        protected readonly IGalaxyContextAsync _context;
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly IUnitOfWorkAsync _unitOfWork;

        #endregion Private Fields

        public EFRepository(IGalaxyContextAsync context, IUnitOfWorkAsync unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;

            var dbContext = context as DbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public virtual void Insert(TEntity entity)
        {
            entity.SyncObjectState(ObjectState.Added);
             _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
            entity.SyncObjectState(ObjectState.Added);
            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Insert(entity);
            }
        }

        public virtual void InsertGraphRange(IEnumerable<TEntity> entities)
        {
          
            _dbSet.AddRange(entities);
            foreach (var entity in entities)
            {
                _context.SyncEntityState(entity);
            }
        }

        public virtual void Update(TEntity entity)
        {
            entity.SyncObjectState(ObjectState.Modified);
            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
        }


        public virtual void Delete(TEntity entity)
        {
            entity.SyncObjectState(ObjectState.Deleted);
            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
        }

        public virtual IQueryFluent<TEntity> Query()
        {
            return new QueryFluent<TEntity>(this);
        }

        public virtual IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject)
        {
            return new QueryFluent<TEntity>(this, queryObject);
        }

        public virtual IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query)
        {
            return new QueryFluent<TEntity>(this, query);
        }

        public virtual IQueryable<TEntity> Queryable()
        {
            return _dbSet;
        }

        public virtual IQueryable<TEntity> QueryableNoTrack()
        {
            return _dbSet.AsNoTracking();
        }

        public virtual IQueryable<TEntity> QueryableWithNoFilter()
        {
            return _dbSet.IgnoreQueryFilters();
        }

       
        public virtual IQueryable<TEntity> ExecuteQuery(string sqlQuery) {
            // Todo : this method is obsolete
            //return _dbSet.FromSql<TEntity>(sqlQuery);
            return this._dbSet;
        }


        public virtual IRepository<T> GetRepository<T>() where T : class, IAggregateRoot, IObjectState
        {
            return _unitOfWork.Repository<T>();
        }

        public virtual async Task<bool> DeleteAsync(params object[] keyValues)
        {
            return await DeleteAsync(CancellationToken.None, keyValues);
        }

 
        internal IQueryable<TEntity> Select(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1)*pageSize.Value).Take(pageSize.Value);
            }
            return query;
        }

        internal async Task<IEnumerable<TEntity>> SelectAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? page = null,
            int? pageSize = null)
        {
            return await Select(filter, orderBy, includes, page, pageSize).ToListAsync();
        }

        public virtual void InsertOrUpdateGraph(TEntity entity)
        {
            SyncObjectGraph(entity);
            _entitesChecked = null;
            _dbSet.Attach(entity);
        }

        HashSet<object> _entitesChecked; // tracking of all process entities in the object graph when calling SyncObjectGraph

        private void SyncObjectGraph(object entity) // scan object graph for all 
        {
            if(_entitesChecked == null)
                _entitesChecked = new HashSet<object>();

            if (_entitesChecked.Contains(entity))
                return;

            _entitesChecked.Add(entity);

            var objectState = entity as IObjectState;

            if (objectState != null && objectState.ObjectState == ObjectState.Added)
                _context.SyncObjectState((IObjectState)entity);

            // Set tracking state for child collections
            foreach (var prop in entity.GetType().GetProperties())
            {
                // Apply changes to 1-1 and M-1 properties
                var trackableRef = prop.GetValue(entity, null) as IObjectState;
                if (trackableRef != null)
                {
                    if(trackableRef.ObjectState == ObjectState.Added)
                        _context.SyncObjectState((IObjectState) entity);

                    SyncObjectGraph(prop.GetValue(entity, null));
                }

                // Apply changes to 1-M properties
                var items = prop.GetValue(entity, null) as IEnumerable<IObjectState>;
                if (items == null) continue;

                foreach (var item in items)
                    SyncObjectGraph(item);
            }
        }

      
        public virtual async Task<TEntity> FindAsync(params object[] keyValues)
        {
            return await _dbSet.FindAsync(keyValues);
        }
        public virtual async Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return await _dbSet.FindAsync(cancellationToken, keyValues);
        }

 
        public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            var entity = await FindAsync(cancellationToken, keyValues);

            if (entity == null)
            {
                return false;
            }
            entity.SyncObjectState(ObjectState.Deleted);
            _dbSet.Attach(entity);

            return true;
        }
            public virtual TEntity Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public virtual IQueryable<TEntity> SelectQuery(string query, params object[] parameters)
        {
            // Todo: This method is absolote
            //   return _dbSet.FromSql(query, parameters).AsQueryable();
            return _dbSet;
        }

            public virtual void Delete(object id)
        {
            var entity = _dbSet.Find(id);
            Delete(entity);
        }
    }
}