﻿using Galaxy.Domain;
using Galaxy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Galaxy.Repositories
{
     public interface IRepository<TEntity, TPrimaryKey>: IRepository  where TEntity : class, IAggregateRoot, IObjectState
    {
        TEntity Find(params object[] keyValues);
        IQueryable<TEntity> SelectQuery(string query, params object[] parameters);
        TEntity Insert(TEntity entity, TPrimaryKey identifier = default);
        void InsertRange(IEnumerable<TEntity> entities);
        void InsertOrUpdateGraph(TEntity entity);
        void InsertGraphRange(IEnumerable<TEntity> entities);
        TEntity Update(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity); 
        IQueryable<TEntity> Queryable();
        IQueryable<TEntity> QueryableNoTrack();
        IQueryable<TEntity> QueryableWithNoFilter();
        IQueryable<TEntity> ExecuteQuery(string sqlQuery); 
    }
}
