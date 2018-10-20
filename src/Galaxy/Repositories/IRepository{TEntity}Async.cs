using Galaxy.Domain;
using Galaxy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Galaxy.Repositories
{
    public interface IRepositoryAsync<TEntity> : IRepositoryAsync<TEntity, int> where TEntity : class, IAggregateRoot, IObjectState
    { 

    }
}
