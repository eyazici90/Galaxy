using Galaxy.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.EntityFrameworkCore.EntityConfigurations
{
    public abstract class GalaxyBaseEntityTypeConfiguration<TEntity> : GalaxyBaseEntityTypeConfiguration<TEntity, int>
         where TEntity : class, IEntity<int>
    {

    }
}
