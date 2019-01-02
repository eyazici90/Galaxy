using Galaxy.Domain;
using Galaxy.Infrastructure;
using Galaxy.ObjectMapping;
using Galaxy.Repositories;
using Galaxy.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Application
{
    public class CrudAppServiceAsync<TEntityDto, TKey, TEntity> : CrudAppService<TEntityDto, TKey, TEntity>, ICrudAppServiceAsync
        where TEntityDto : IEntityDto<TKey>
        where TEntity : class, IEntity<TKey>, IAggregateRoot, IObjectState
    {
        public CrudAppServiceAsync(IRepositoryAsync<TEntity, TKey> repositoryAsync
            , IObjectMapper objectMapper
            , IUnitOfWorkAsync unitOfWork) : base(repositoryAsync, objectMapper, unitOfWork)
        {
        }

        public virtual async Task<TEntityDto> AddAsync(Func<Task<TEntity>> when)
        {
            var aggregate = await when();
            await _repositoryAsync.InsertAsync(aggregate);
            await this._unitOfWorkAsync.SaveChangesAsync();
            return base._objectMapper.MapTo<TEntityDto>(
                aggregate
                );
        }

        public virtual async Task<TEntityDto> UpdateAsync(TKey id, Func<TEntity, Task> when)
        {
            TEntity aggregate = await base._repositoryAsync.FindAsync(id);
            await when(aggregate);
            _repositoryAsync.Update(aggregate);
            await this._unitOfWorkAsync.SaveChangesAsync();
            return base._objectMapper.MapTo<TEntityDto>(
                aggregate
                );
        }

        public virtual async Task DeleteAsync(TKey id)
        {
            await base._repositoryAsync.DeleteAsync(id);
            await this._unitOfWorkAsync.SaveChangesAsync();
        }
    }


}
