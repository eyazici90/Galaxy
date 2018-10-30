using System;
using System.Collections.Generic;
using System.Text;
using Galaxy.Domain;
using Galaxy.Infrastructure;
using Galaxy.Repositories;
using Galaxy.UnitOfWork;

namespace Galaxy.Commands
{
    public abstract class CommandHandlerBase<TAggregateRoot> : CommandHandlerBase<TAggregateRoot, int>
        where TAggregateRoot : class, IAggregateRoot, IObjectState
    {
        public CommandHandlerBase(IUnitOfWorkAsync unitOfWorkAsync
            , IRepositoryAsync<TAggregateRoot,int> aggregateRootRepository) : base(unitOfWorkAsync, aggregateRootRepository)
        {
        }
    }
}
