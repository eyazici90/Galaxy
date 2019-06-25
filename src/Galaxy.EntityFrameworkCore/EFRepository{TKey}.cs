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
    public  class EFRepository<TEntity,TKey> : EFRepository<TEntity> , IRepositoryAsync<TEntity, TKey>, IRepository<TEntity, TKey> where TEntity : class, IAggregateRoot, IObjectState
    {
      
        public EFRepository(IGalaxyContextAsync context, IUnitOfWorkAsync unitOfWork) : base(context,unitOfWork)
        {
        }

        public async Task<TEntity> FindAsync(TKey keyValues)
        {
            return await DbSet.FindAsync(keyValues);
        }

        public  async Task<TEntity> FindAsync(TKey keyValues, CancellationToken cancellationToken)
        {
            return await DbSet.FindAsync(keyValues, cancellationToken);
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

        public virtual TEntity Insert(TEntity entity, TKey identifier = default)
        {
            entity.SyncObjectState(ObjectState.Added);
            DbSet.Attach(entity);
            Context.SyncObjectState(entity);
            return entity;
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity, TKey identifier = default) => await Task.FromResult(Insert(entity));

    }
}