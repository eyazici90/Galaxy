using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Dapper
{
    public interface IDapperRepository<TEntity> : IDapperRepository<TEntity,int> where TEntity : class
    {

    }
}
