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
    public class EFRepository<TEntity> : EFRepository<TEntity, int>, IReadOnlyRepositoryAsync<TEntity>, IRepositoryAsync<TEntity>, IRepository<TEntity> where TEntity : class, IAggregateRoot, IObjectState
    {
        public EFRepository(IGalaxyContextAsync context
            , IUnitOfWorkAsync unitOfWorkAsync) : base(context, unitOfWorkAsync)
        {
        }
    }
}