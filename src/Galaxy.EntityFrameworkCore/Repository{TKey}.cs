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
    public  class Repository<TEntity,TKey> : Repository<TEntity> where TEntity : class, IAggregateRoot, IObjectState
    {
      
        public Repository(IGalaxyContextAsync context, IUnitOfWorkAsync unitOfWork) : base(context,unitOfWork)
        {
        }

        public async Task<TEntity> FindAsync(TKey keyValues)
        {
            return await _dbSet.FindAsync(keyValues);
        }
        public  async Task<TEntity> FindAsync(CancellationToken cancellationToken, TKey keyValues)
        {
            return await _dbSet.FindAsync(cancellationToken, keyValues);
        }


        public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, TKey keyValues)
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
        public virtual TEntity Find(TKey keyValues)
        {
            return _dbSet.Find(keyValues);
        }
        
        public virtual void Delete(TKey id)
        {
            var entity = _dbSet.Find(id);
            Delete(entity);
        }
    }
}