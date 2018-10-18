using Galaxy.Domain;
using Galaxy.Infrastructure;
using Galaxy.ObjectMapping;
using Galaxy.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Galaxy.Application
{
    public abstract class QueryAppService<TEntityDto, TKey, TEntity> : IQueryAppService<TEntityDto, TKey, TEntity>
         where TEntityDto : IEntityDto<TKey>
         where TEntity : class, IEntity<TKey>, IAggregateRoot, IObjectState

    {
        protected readonly IRepositoryAsync<TEntity,TKey> _repositoryAsync;
        protected readonly IObjectMapper _objectMapper;
 

        public QueryAppService(IRepositoryAsync<TEntity,TKey> repositoryAsync
            , IObjectMapper objectMapper)
        {
            this._repositoryAsync = repositoryAsync ?? throw new ArgumentNullException(nameof(repositoryAsync));
            this._objectMapper = objectMapper ?? throw new ArgumentNullException(nameof(objectMapper));
        }
        

        public IQueryable<TEntityDto> Queryable()
        {
            return this._objectMapper.MapTo<TEntityDto>(
                    this._repositoryAsync.Queryable()
                );
        }

        public IQueryable<TEntityDto> QueryableNoTrack()
        {
            return this._objectMapper.MapTo<TEntityDto>(
                    this._repositoryAsync.QueryableNoTrack()
                );
        }

        public IQueryable<TEntityDto> QueryableWithNoFilter()
        {
            return this._objectMapper.MapTo<TEntityDto>(
                    this._repositoryAsync.QueryableWithNoFilter()
                );
        }

        public TEntityDto Find(TKey id)
        {
            return this._objectMapper.MapTo<TEntityDto>(
                    this._repositoryAsync.Find(id)
                );
        }

        public TEntityDto Get(TKey id)
        {
            return this._objectMapper.MapTo<TEntityDto>(
                   this._repositoryAsync.Find(id)
               );
        }

        public IList<TEntityDto> GetAll()
        {
            return this._objectMapper.MapTo<IList<TEntityDto>>(
                    this._repositoryAsync.Queryable().ToList()
                );
        }

        public IQueryable<TEntityDto> GetAll(Expression<Func<TEntity, bool>> whereCondition = default)
        {
            if (whereCondition == default)
            {
                return this._objectMapper.MapTo<TEntityDto>(
                                   this._repositoryAsync.Queryable()
                               );
            }
            return this._objectMapper.MapTo<TEntityDto>(
                   this._repositoryAsync.Queryable()
                        .Where(whereCondition)
               );
        }
    }
}
