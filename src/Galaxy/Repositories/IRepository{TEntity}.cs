using Galaxy.Domain;
using Galaxy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Repositories
{
    public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : class, IAggregateRoot, IObjectState
    {
       
    }
}
