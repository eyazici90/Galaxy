using Galaxy.Domain;
using Galaxy.Infrastructure;
using Galaxy.Repositories;
using System;
using System.Data;

namespace Galaxy.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Attach(object entity);
        int SaveChanges();
        void Dispose(bool disposing);
        IRepository<TEntity> Repository<TEntity>() where TEntity : class, IAggregateRoot, IObjectState;
        void BeginTransaction(IUnitOfWorkOptions unitOfWorkOptions = default);
        bool Commit();
        void Rollback();
        bool CheckIfThereIsAvailableTransaction();
    }
}