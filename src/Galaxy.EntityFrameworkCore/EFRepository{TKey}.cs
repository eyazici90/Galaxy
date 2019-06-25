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
    public  class EFRepository<TEntity,TKey> :  IRepositoryAsync<TEntity, TKey>, IRepository<TEntity, TKey> where TEntity : class, IAggregateRoot, IObjectState
    {
        #region Private Fields
        protected readonly IGalaxyContextAsync Context;
        protected readonly DbSet<TEntity> DbSet;
        protected readonly IUnitOfWorkAsync UnitOfWorkAsync;

        #endregion Private Fields

        public EFRepository(IGalaxyContextAsync context, IUnitOfWorkAsync unitOfWorkAsync)
        {
            Context = context;
            UnitOfWorkAsync = unitOfWorkAsync;

            var dbContext = context as DbContext;
            DbSet = dbContext.Set<TEntity>();
        } 
      

        public async Task<TEntity> FindAsync(TKey keyValues)
        {
            return await DbSet.FindAsync(keyValues);
        }

        public  async Task<TEntity> FindAsync(TKey keyValues, CancellationToken cancellationToken)
        {
            return await DbSet.FindAsync(keyValues, cancellationToken);
        }

        public virtual async Task<TEntity> FindAsync(params object[] keyValues)
        {
            return await DbSet.FindAsync(keyValues);
        }

        public virtual async Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return await DbSet.FindAsync(keyValues, cancellationToken);
        }

        public virtual TEntity Insert(TEntity entity, TKey identifier = default)
        {
            entity.SyncObjectState(ObjectState.Added);
            DbSet.Attach(entity);
            Context.SyncObjectState(entity);
            return entity;
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity, TKey identifier = default) => await Task.FromResult(Insert(entity));

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Insert(entity);
            }
        }

        public virtual void InsertGraphRange(IEnumerable<TEntity> entities)
        {

            DbSet.AddRange(entities);
            foreach (var entity in entities)
            {
                Context.SyncEntityState(entity);
            }
        }

        public virtual TEntity Update(TEntity entity)
        {
            entity.SyncObjectState(ObjectState.Modified);
            DbSet.Attach(entity);
            Context.SyncObjectState(entity);
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity) => await Task.FromResult(Update(entity));

        public virtual void Delete(TEntity entity)
        {
            entity.SyncObjectState(ObjectState.Deleted);
            DbSet.Attach(entity);
            Context.SyncObjectState(entity);
        }
 

        public virtual IQueryable<TEntity> Queryable() =>
             DbSet;
        

        public virtual IQueryable<TEntity> QueryableNoTrack() =>
             DbSet.AsNoTracking();
        

        public virtual IQueryable<TEntity> QueryableWithNoFilter() => 
            DbSet.IgnoreQueryFilters();
        

        public virtual IQueryable<TEntity> ExecuteQuery(string sqlQuery) =>
             DbSet.FromSql<TEntity>(sqlQuery);
        

        public virtual IRepository<T> GetRepository<T>() where T : class, IAggregateRoot, IObjectState =>
             UnitOfWorkAsync.Repository<T>();
        

        public virtual async Task<bool> DeleteAsync(params object[] keyValues) =>
             await DeleteAsync(CancellationToken.None, keyValues);
        


        internal IQueryable<TEntity> Select(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = DbSet;

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
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
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
            DbSet.Attach(entity);
        }


        public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            var entity = await FindAsync(cancellationToken, keyValues);

            if (entity == null)
            {
                return false;
            }
            entity.SyncObjectState(ObjectState.Deleted);
            DbSet.Attach(entity);
            Context.SyncObjectState(entity);
            return true;
        }
        public virtual TEntity Find(params object[] keyValues) =>
             DbSet.Find(keyValues);
        

        public virtual IQueryable<TEntity> SelectQuery(string query, params object[] parameters) =>
             DbSet.FromSql(query, parameters).AsQueryable();
        

        public virtual void Delete(object id)
        {
            var entity = DbSet.Find(id);
            Delete(entity);
        }


        HashSet<object> _entitesChecked;

        private void SyncObjectGraph(object entity)
        {
            if (_entitesChecked == null)
                _entitesChecked = new HashSet<object>();

            if (_entitesChecked.Contains(entity))
                return;

            _entitesChecked.Add(entity);

            var objectState = entity as IObjectState;

            if (objectState != null && objectState.ObjectState == ObjectState.Added)
                Context.SyncObjectState((IObjectState)entity);

            foreach (var prop in entity.GetType().GetProperties())
            {
                var trackableRef = prop.GetValue(entity, null) as IObjectState;
                if (trackableRef != null)
                {
                    if (trackableRef.ObjectState == ObjectState.Added)
                        Context.SyncObjectState((IObjectState)entity);

                    SyncObjectGraph(prop.GetValue(entity, null));
                }

                var items = prop.GetValue(entity, null) as IEnumerable<IObjectState>;
                if (items == null) continue;

                foreach (var item in items)
                    SyncObjectGraph(item);
            }
        }

        public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, TKey keyValues)
        {
            var entity = await FindAsync(keyValues, cancellationToken);

            if (entity == null)
            {
                return false;
            }
            entity.SyncObjectState(ObjectState.Deleted);
            DbSet.Attach(entity);
            Context.SyncObjectState(entity);
            return true;
        }

        public virtual TEntity Find(TKey keyValues)
        {
            return DbSet.Find(keyValues);
        }
        
        public virtual void Delete(TKey id)
        {
            var entity = DbSet.Find(id);
            Delete(entity);
        }


    }
}