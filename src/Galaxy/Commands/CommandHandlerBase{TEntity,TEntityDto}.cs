using System;
using System.Collections.Generic;
using System.Text;
using Galaxy.Domain;
using Galaxy.Infrastructure;
using Galaxy.ObjectMapping;
using Galaxy.Repositories;
using Galaxy.UnitOfWork;

namespace Galaxy.Commands
{
    public abstract class CommandHandlerBase<TAggregateRoot, TEntityDto> : CommandHandlerBase<TAggregateRoot, TEntityDto, int>
        where TAggregateRoot : class, IAggregateRoot, IObjectState
    {
        public CommandHandlerBase(IUnitOfWorkAsync unitOfWorkAsync
            , IRepositoryAsync<TAggregateRoot,int> aggregateRootRepository
            , IObjectMapper objectMapper) : base(unitOfWorkAsync, aggregateRootRepository, objectMapper)
        {
        }
    }
}
