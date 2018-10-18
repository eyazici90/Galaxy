using Galaxy.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.EntityFrameworkCore.EntityConfigurations
{
    public abstract class GalaxyBaseEntityTypeConfigration<TEntity> : GalaxyBaseEntityTypeConfigration<TEntity, int> where TEntity : Entity<int>
    {

    }
}
