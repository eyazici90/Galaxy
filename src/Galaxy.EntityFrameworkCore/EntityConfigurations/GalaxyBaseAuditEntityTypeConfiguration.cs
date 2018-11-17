using Galaxy.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.EntityFrameworkCore.EntityConfigurations
{
    public abstract class GalaxyBaseAuditEntityTypeConfiguration<TEntity> : GalaxyBaseAuditEntityTypeConfiguration<TEntity, int>
        where TEntity :  AggregateRootEntity<int>
    {

    }
}
