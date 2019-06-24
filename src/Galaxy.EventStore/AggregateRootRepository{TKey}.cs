using EventStore.ClientAPI;
using Galaxy.Domain;
using Galaxy.Infrastructure;
using Galaxy.Repositories;
using Galaxy.Serialization;
using Galaxy.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Galaxy.EventStore
{
    public class AggregateRootRepository<TAggregateRoot, TKey> : IRepositoryAsync<TAggregateRoot, TKey>, IRepository<TAggregateRoot, TKey>
        where TAggregateRoot : class, IAggregateRoot, IObjectState
    {

        private readonly IUnitOfWorkAsync _unitOfworkAsync;
        private readonly IEventStoreConnection _connection;
        private readonly ISerializer _serializer;
        public AggregateRootRepository(IUnitOfWorkAsync unitOfworkAsync
            , IEventStoreConnection connection
            , ISerializer serializer)
        {
            _unitOfworkAsync = unitOfworkAsync ?? throw new ArgumentNullException(nameof(unitOfworkAsync));
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public async Task<TAggregateRoot> FindAsync(params object[] keyValues)
        {
            var existingAggregate = _unitOfworkAsync.AttachedObject(keyValues[0].ToString());
            if (existingAggregate != null)
            {
                return (TAggregateRoot)(((Aggregate)existingAggregate).Root);
            }
            var streamName = StreamExtensions.GetStreamName(typeof(TAggregateRoot), keyValues[0].ToString());

            var version = StreamPosition.Start;

            StreamEventsSlice slice =
                 await
                     _connection.ReadStreamEventsForwardAsync(streamName, version, 100, false);
            if (slice.Status == SliceReadStatus.StreamDeleted || slice.Status == SliceReadStatus.StreamNotFound)
            {
                throw new AggregateNotFoundException($"Aggregate not found by {streamName}");
            }

            TAggregateRoot root = (TAggregateRoot)Activator.CreateInstance(typeof(TAggregateRoot), true);

            slice.Events.ToList().ForEach(e =>
            {
                var resolvedEvent = this._serializer.Deserialize(Type.GetType(e.Event.EventType, true), Encoding.UTF8.GetString(e.Event.Data));
                (root as IEntity).ApplyEvent(resolvedEvent);
            });

            while (!slice.IsEndOfStream)
            {
                slice =
                    await
                        _connection.ReadStreamEventsForwardAsync(streamName, slice.NextEventNumber, 100,
                            false);
                slice.Events.ToList().ForEach(e =>
                {
                    var resolvedEvent = this._serializer.Deserialize(Type.GetType(e.Event.EventType, true), Encoding.UTF8.GetString(e.Event.Data));
                    (root as IEntity).ApplyEvent(resolvedEvent);
                });
            }

          (root as IEntity).ClearEvents();

            var aggregate = new Aggregate(keyValues[0].ToString(), (int)slice.LastEventNumber, root);

            this._unitOfworkAsync.Attach(aggregate);

            return root;
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public void Delete(TAggregateRoot entity)
        {
            _unitOfworkAsync.Attach(entity);
        }

        public Task<bool> DeleteAsync(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TAggregateRoot> ExecuteQuery(string sqlQuery)
        {
            throw new NotImplementedException();
        }

        public  TAggregateRoot Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

       
        
        public async Task<TAggregateRoot> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return await this.FindAsync(keyValues);
        }

        public TAggregateRoot Insert(TAggregateRoot entity)
        {
            this._unitOfworkAsync.Attach(new Aggregate(Guid.NewGuid().ToString(), (int)ExpectedVersion.NoStream, entity));
            return entity;
        }

        public void InsertGraphRange(IEnumerable<TAggregateRoot> entities)
        {
            foreach (var entity in entities)
            {
                this._unitOfworkAsync
                    .Attach(new Aggregate(Guid.NewGuid().ToString(), (int)ExpectedVersion.NoStream, entity));
            }
        }

        public void InsertOrUpdateGraph(TAggregateRoot entity)
        {
            throw new NotImplementedException();
        }

        public void InsertRange(IEnumerable<TAggregateRoot> entities)
        {
            throw new NotImplementedException();
        }

        public IQueryFluent<TAggregateRoot> Query(IQueryObject<TAggregateRoot> queryObject)
        {
            throw new NotImplementedException();
        }

        public IQueryFluent<TAggregateRoot> Query(Expression<Func<TAggregateRoot, bool>> query)
        {
            throw new NotImplementedException();
        }

        public IQueryFluent<TAggregateRoot> Query()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TAggregateRoot> Queryable()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TAggregateRoot> QueryableNoTrack()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TAggregateRoot> QueryableWithNoFilter()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TAggregateRoot> SelectQuery(string query, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public TAggregateRoot Update(TAggregateRoot entity) =>
            UpdateAsync(entity).ConfigureAwait(false)
             .GetAwaiter().GetResult();
         

        public async Task<TAggregateRoot> InsertAsync(TAggregateRoot entity)
        {
            this.Insert(entity);
            return entity;
        }

        public async Task<TAggregateRoot> UpdateAsync(TAggregateRoot entity)
        { 
            return await Task.FromResult(entity);
        }

      
    }
}
