using Galaxy.Domain;
using Galaxy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Repositories
{
    public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : class, IAggregateRoot, IObjectState
    {
    }
}
