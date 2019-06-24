using EventStore.ClientAPI;
using Galaxy.Domain;
using Galaxy.Extensions;
using Galaxy.Infrastructure;
using Galaxy.Repositories;
using Galaxy.Serialization;
using Galaxy.UnitOfWork;
using System; 
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.EventStore
{
    public class EventStoreSnapshotter<TAggregateRoot, TKey, TSnapshot> : ISnapshotter
         where TAggregateRoot : class, IAggregateRoot, IObjectState
    {
        private readonly IRepositoryAsync<TAggregateRoot, TKey> _rootRepo;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly Func<IEventStoreConnection> _getConnection; 
        private readonly Func<string, string> _snapshotNameResolve;
        private readonly Func<ResolvedEvent, bool> _strategy;
        private readonly ISerializer _serializer;

        public EventStoreSnapshotter(
            IRepositoryAsync<TAggregateRoot, TKey> rootRepo,
            IUnitOfWorkAsync unitOfWorkAsync,
            Func<IEventStoreConnection> getConnection,
            Func<ResolvedEvent, bool> strategy,
            Func<string, string> snapshotNameResolve,
            ISerializer serializer
            )
        {
            _rootRepo = rootRepo;
            _unitOfWorkAsync = unitOfWorkAsync;
            _strategy = strategy;
            _snapshotNameResolve = snapshotNameResolve; 
            _getConnection = getConnection;
            _serializer = serializer;
        }

        public bool ShouldTakeSnapshot(Type aggregateType, ResolvedEvent e) =>
            typeof(ISnapshotable).IsAssignableFrom(aggregateType) && _strategy(e);

        public async Task TakeSnapshot(string streamId)
        {
            TAggregateRoot aggregateRoot = await _rootRepo.FindAsync(StreamExtensions.GetIdentifierFromStreamId(streamId));

            var aggregate = (Aggregate)this._unitOfWorkAsync.AttachedObject(streamId); 

            if (aggregateRoot == null) { throw new AggregateNotFoundException($"Aggregate not found by {streamId}"); }

            var changes = new EventData(
                                        Guid.NewGuid(),
                                        typeof(TSnapshot).TypeQualifiedName(),
                                        true,
                                        Encoding.UTF8.GetBytes(_serializer.Serialize(((ISnapshotable)aggregateRoot).TakeSnapshot())),
                                        Encoding.UTF8.GetBytes(_serializer.Serialize(new EventMetadata
                                        {
                                            AggregateAssemblyQualifiedName = typeof(TAggregateRoot).AssemblyQualifiedName,
                                            AggregateType = typeof(TAggregateRoot).Name,
                                            TimeStamp = DateTime.Now,
                                            IsSnapshot = true,
                                            Version = aggregate.ExpectedVersion
                                        })
                                       ));

            string snapshotStream = _snapshotNameResolve(streamId);
            await _getConnection().AppendToStreamAsync(snapshotStream, ExpectedVersion.Any, changes);
        } 
    }
}
