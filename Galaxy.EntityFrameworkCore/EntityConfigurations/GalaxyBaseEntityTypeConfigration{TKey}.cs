using Galaxy.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.EntityFrameworkCore.EntityConfigurations
{
    public abstract class GalaxyBaseEntityTypeConfigration<TEntity, TPrimary>
       : IEntityTypeConfiguration<TEntity> where TEntity : Entity<TPrimary>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();

            builder.Ignore(e => e.ObjectState);
            builder.Ignore(e => e.DomainEvents);
        }
    }
}
