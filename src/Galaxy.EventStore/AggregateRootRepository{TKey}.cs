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

        private async Task<TAggregateRoot> ApplyEventsToRoot(string streamName, TAggregateRoot aggregateRoot)
        {
            var sliceStart = StreamPosition.Start;
            StreamEventsSlice slice;

            do
            {
                slice = await _connection.ReadStreamEventsForwardAsync(streamName, sliceStart, 200, false);

                slice.Events.ToList().ForEach(e=> 
                {
                    var resolvedEvent = this._serializer.Deserialize(Type.GetType(e.Event.EventType, true), Encoding.UTF8.GetString(e.Event.Data));
                    (aggregateRoot as IEntity).ApplyEvent(resolvedEvent);
                });
                 
                sliceStart = Convert.ToInt32(slice.NextEventNumber);

            } while (!slice.IsEndOfStream);
            (aggregateRoot as IEntity).ClearEvents();
            return aggregateRoot;
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

        public async Task<TAggregateRoot> FindAsync(params object[] keyValues)
        {
            string streamName = StreamExtensions.GetStreamName(typeof(TAggregateRoot), keyValues[0].ToString());
            StreamEventsSlice slice =
                await
                    _connection.ReadStreamEventsForwardAsync(streamName, StreamPosition.Start, 100,
                        false);

            if (slice.Status == SliceReadStatus.StreamDeleted || slice.Status == SliceReadStatus.StreamNotFound)
            {
                return null;
            } 

            var aggregateRoot = (TAggregateRoot)Activator.CreateInstance(typeof(TAggregateRoot), true);

            aggregateRoot = await ApplyEventsToRoot(streamName, aggregateRoot);

            var aggregate = new Aggregate(streamName, (int)slice.LastEventNumber, aggregateRoot);

            this._unitOfworkAsync.Attach(aggregate);

            return aggregateRoot;
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
