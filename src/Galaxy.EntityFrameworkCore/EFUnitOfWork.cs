﻿#region
using System;
using System.Collections.Generic;
using System.Transactions;
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
    public class EFUnitOfWork : IUnitOfWorkAsync
    {
        #region Private Fields
        private readonly IMediator _mediator;
        private IGalaxyContextAsync _dataContext;
        private IAppSessionContext _session;
        private bool _disposed;
        private IDbContextTransaction _transaction;
        private Dictionary<string, dynamic> _repositories;
     
        #endregion Private Fields

        #region Constuctor

        public EFUnitOfWork(IGalaxyContextAsync dataContext
            , IAppSessionContext session
            , IMediator mediator)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            _session = session ?? throw new ArgumentNullException(nameof(session));
            _repositories = new Dictionary<string, dynamic>();
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        
        #endregion Constuctor

        public bool CheckIfThereIsAvailableTransaction()
        {
            return this._dataContext.CheckIfThereIsAvailableTransaction();
        }

        public void Attach(object entity)
        {
            this._dataContext.Attach(entity);
        }
        
        public int SaveChanges()
        {
            this._dataContext.SyncObjectsAuditPreCommit(this._session);
            var changes = _dataContext.SaveChanges();
            this._dataContext.DispatchNotificationsAsync(this._mediator).ConfigureAwait(false)
             .GetAwaiter().GetResult();
            return changes;
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IAggregateRoot, IObjectState
        {
            return RepositoryConcrete<TEntity>();
        }

        public async Task<int> SaveChangesAsync()
        {
            this._dataContext.SyncObjectsAuditPreCommit(this._session);
            var changes = await  _dataContext.SaveChangesAsync();
            await this._dataContext.DispatchNotificationsAsync(this._mediator);
            return changes;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            this._dataContext.SyncObjectsAuditPreCommit(this._session);
            var changes = await _dataContext.SaveChangesAsync(cancellationToken);
            await this._dataContext.DispatchNotificationsAsync(this._mediator);
            return changes;
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

            var repositoryType = typeof(EFRepository<>);

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

            var repositoryType = typeof(EFRepository<>);

            _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dataContext, this));

            return _repositories[type];
        }

        #region Unit of Work Transactions

        public void BeginTransaction(IUnitOfWorkOptions unitOfWorkOptions = default)
        {
            if (unitOfWorkOptions == default)
            { 
                _transaction = ((DbContext)_dataContext).Database.BeginTransaction();
            }
            else
            {
                if (unitOfWorkOptions.Timeout.HasValue)
                {
                    ((DbContext)_dataContext).Database.SetCommandTimeout(unitOfWorkOptions.Timeout.Value);
                }
                
                _transaction = unitOfWorkOptions.IsolationLevel.HasValue ? ((DbContext)_dataContext).Database.BeginTransaction(unitOfWorkOptions.IsolationLevel.Value) 
                                                                         : ((DbContext)_dataContext).Database.BeginTransaction();              
            }
        }

        public async Task BeginTransactionAsync(IUnitOfWorkOptions unitOfWorkOptions = default)
        {
            if (unitOfWorkOptions == default)
            {
                _transaction = await ((DbContext)_dataContext).Database.BeginTransactionAsync();
            }
            else
            { 
                if (unitOfWorkOptions.Timeout.HasValue)
                {
                    ((DbContext)_dataContext).Database.SetCommandTimeout(unitOfWorkOptions.Timeout.Value);
                }
                _transaction = unitOfWorkOptions.IsolationLevel.HasValue ? await ((DbContext)_dataContext).Database.BeginTransactionAsync(unitOfWorkOptions.IsolationLevel.Value)
                                                                         : await ((DbContext)_dataContext).Database.BeginTransactionAsync();
            } 
        }

        public bool Commit()
        {
            this._dataContext.SyncObjectsAuditPreCommit(this._session);

            var changes =  _dataContext.SaveChangesAsync().ConfigureAwait(false)
                .GetAwaiter().GetResult();
            
            _transaction.Commit();

            this._dataContext.DispatchNotificationsAsync(this._mediator).ConfigureAwait(false)
             .GetAwaiter().GetResult();

            return true;
        }

        public async Task<bool> CommitAsync()
        {
            this._dataContext.SyncObjectsAuditPreCommit(this._session);

            var changes = await _dataContext.SaveChangesAsync();

            _transaction.Commit();

            await this._dataContext.DispatchNotificationsAsync(this._mediator);

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
                if (_dataContext != null)
                {
                    _dataContext.Dispose();
                    _dataContext = null;
                }
            }
            _disposed = true;
        }
    }
}