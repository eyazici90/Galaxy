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
    public abstract class QueryAppService<TEntity, TEntityDto, TKey> : IQueryAppService<TEntity, TEntityDto, TKey>
         where TEntity : class, IEntity<TKey>, IAggregateRoot, IObjectState
    {
        protected readonly IRepositoryAsync<TEntity,TKey> RepositoryAsync;
        protected readonly IObjectMapper ObjectMapper;

        public QueryAppService(IRepositoryAsync<TEntity,TKey> repositoryAsync
            , IObjectMapper objectMapper)
        {
            this.RepositoryAsync = repositoryAsync ?? throw new ArgumentNullException(nameof(repositoryAsync));
            this.ObjectMapper = objectMapper ?? throw new ArgumentNullException(nameof(objectMapper));
        }
        
        public virtual IQueryable<TEntityDto> Queryable()
        {
            return this.ObjectMapper.MapTo<TEntityDto>(
                    this.RepositoryAsync.Queryable()
                );
        }

        public virtual IQueryable<TEntityDto> QueryableNoTrack()
        {
            return this.ObjectMapper.MapTo<TEntityDto>(
                    this.RepositoryAsync.QueryableNoTrack()
                );
        }

        public virtual IQueryable<TEntityDto> QueryableWithNoFilter()
        {
            return this.ObjectMapper.MapTo<TEntityDto>(
                    this.RepositoryAsync.QueryableWithNoFilter()
                );
        }

        public virtual TEntityDto Find(TKey id)
        {
            return this.ObjectMapper.MapTo<TEntityDto>(
                    this.RepositoryAsync.Find(id)
                );
        }

        public virtual TEntityDto Get(TKey id)
        {
            return this.ObjectMapper.MapTo<TEntityDto>(
                   this.RepositoryAsync.Find(id)
               );
        }

        public virtual IList<TEntityDto> GetAll()
        {
            return this.ObjectMapper.MapTo<IList<TEntityDto>>(
                    this.RepositoryAsync.Queryable().ToList()
                );
        }

        public virtual IQueryable<TEntityDto> GetAll(Expression<Func<TEntity, bool>> whereCondition = default)
        {
            if (whereCondition == default)
            {
                return this.ObjectMapper.MapTo<TEntityDto>(
                                   this.RepositoryAsync.Queryable()
                               );
            }
            return this.ObjectMapper.MapTo<TEntityDto>(
                   this.RepositoryAsync.Queryable()
                        .Where(whereCondition)
               );
        }
    }
}
