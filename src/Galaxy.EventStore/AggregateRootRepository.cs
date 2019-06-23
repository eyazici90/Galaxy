using EventStore.ClientAPI;
using Galaxy.Domain;
using Galaxy.Infrastructure;
using Galaxy.Repositories;
using Galaxy.Serialization;
using Galaxy.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Galaxy.EventStore
{
    public class AggregateRootRepository<TAggregateRoot> : AggregateRootRepository<TAggregateRoot, int> , IRepositoryAsync<TAggregateRoot>, IRepository<TAggregateRoot>
       where TAggregateRoot : class, IAggregateRoot, IObjectState
    {
        public AggregateRootRepository(IUnitOfWorkAsync unitOfworkAsync
            , IEventStoreConnection connection, ISerializer serializer) 
            : base(unitOfworkAsync, connection, serializer)
        {
        }
         
    }
}
