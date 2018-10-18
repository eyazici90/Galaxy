#region
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MediatR;
using Galaxy.UnitOfWork;
using Galaxy.Domain;
using Galaxy.Infrastructure;
using Galaxy.DataContext;
using Galaxy.Session;
using Galaxy.Repositories;
#endregion

namespace Galaxy.EFCore
{
    public class UnitOfWork : IUnitOfWorkAsync
    {
        #region Private Fields
        private readonly IMediator _mediator;
        private IGalaxyContextAsync _dataContext;
        private IAppSessionBase _session;
        private bool _disposed;
        
        private IDbContextTransaction _transaction;
        private DbContext _dbContext;
        private Dictionary<string, dynamic> _repositories;
     

        #endregion Private Fields

        #region Constuctor/Dispose

        public UnitOfWork(IGalaxyContextAsync dataContext
            , IAppSessionBase session
            , IMediator mediator)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            _session = session ?? throw new ArgumentNullException(nameof(session));
            _repositories = new Dictionary<string, dynamic>();
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (_dbContext != null)
                        {
                            _dbContext.Dispose();
                            _dbContext = null;
                        }
                if (_dataContext != null)
                {
                    _dataContext.Dispose();
                    _dataContext = null;
                }
            }
            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
        }

        #endregion Constuctor/Dispose

        public bool CheckIfThereIsAvailableTransaction()
        {
            return this._dataContext.CheckIfThereIsAvailableTransaction();
        }

        public int SaveChanges()
        {
            this._dataContext.SyncObjectsAuditPreCommit(this._session);
            this._dataContext.DispatchNotificationsAsync(this._mediator).ConfigureAwait(false)
                .GetAwaiter().GetResult();
            return _dataContext.SaveChanges();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IAggregateRoot, IObjectState
        {
            return RepositoryConcrete<TEntity>();
        }

        public async Task<int> SaveChangesAsync()
        {
            this._dataContext.SyncObjectsAuditPreCommit(this._session);
            await this._dataContext.DispatchNotificationsAsync(this._mediator);
            return await  _dataContext.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            this._dataContext.SyncObjectsAuditPreCommit(this._session);
            await  this._dataContext.DispatchNotificationsAsync(this._mediator);
            return await _dataContext.SaveChangesAsync(cancellationToken);
        }

        public IRepository<TEntity> RepositoryConcrete<TEntity>() where TEntity : class, IAggregateRoot, IObjectState
        {

            if (_repositories == null)
            {
                _repositories = new Dictionary<string, dynamic>();
            }

            var type = typeof(TEntity).Name;

            if (_repositories.ContainsKey(type))
            {
                return (IRepository<TEntity>)_repositories[type];
            }

            var repositoryType = typeof(Repository<>);

            _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dataContext, this));

            return _repositories[type];
        }

        public IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class, IAggregateRoot, IObjectState
        {

            if (_repositories == null)
            {
                _repositories = new Dictionary<string, dynamic>();
            }

            var type = typeof(TEntity).Name;

            if (_repositories.ContainsKey(type))
            {
                return (IRepositoryAsync<TEntity>)_repositories[type];
            }

            var repositoryType = typeof(Repository<>);

            _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dataContext, this));

            return _repositories[type];
        }

        #region Unit of Work Transactions

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
              _dbContext = (DbContext)_dataContext;
            _transaction = _dbContext.Database.BeginTransaction();
        }

        public bool Commit()
        {
            _transaction.Commit();
            return true;
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _dataContext.SyncObjectsStatePostCommit();
        }

        public async Task<int> SaveChangesByPassAsync(CancellationToken cancellationToken = default)
        {
          return await this._dataContext.SaveChangesByPassedAsync(cancellationToken);
        }

      

        #endregion
    }
}