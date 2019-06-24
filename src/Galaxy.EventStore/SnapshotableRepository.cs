using EventStore.ClientAPI;
using Galaxy.Domain;
using Galaxy.Infrastructure;
using Galaxy.Repositories;
using Galaxy.Serialization;
using Galaxy.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.EventStore
{

    public class SnapshotableRepository<TAggregateRoot> : SnapshotableRepository<TAggregateRoot, int>, IRepositoryAsync<TAggregateRoot>, IRepository<TAggregateRoot>
      where TAggregateRoot : class, IAggregateRoot, ISnapshotable, IObjectState
    {
        public SnapshotableRepository(IAsyncSnapshotReader snapshotReader, IUnitOfWorkAsync unitOfworkAsync
            , IEventStoreConnection connection, ISerializer serializer) 
            : base(snapshotReader, unitOfworkAsync, connection, serializer)
        {
        }
    }
}
