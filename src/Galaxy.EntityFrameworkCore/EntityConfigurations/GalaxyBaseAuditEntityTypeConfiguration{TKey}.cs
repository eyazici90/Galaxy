﻿using Galaxy.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Galaxy.EntityFrameworkCore.EntityConfigurations
{
    
    public abstract class GalaxyBaseAuditEntityTypeConfiguration<TEntity, TPrimary>
     : IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity<TPrimary>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();

            if (typeof(IConcurrencyStamp).GetTypeInfo().IsAssignableFrom(typeof(TEntity)))
            {
                builder
                 .Property(e => ((IConcurrencyStamp)e).ConcurrencyStamp)
                 .IsConcurrencyToken()
                 .HasColumnName(nameof(IConcurrencyStamp.ConcurrencyStamp));
            }

            builder.Ignore(e => e.ObjectState);
            builder.Ignore(e => e.DomainEvents);
        }

        public virtual void Configure(EntityTypeBuilder<TEntity> builder, bool ignoreIdconfigurations = default)
        {
            if (!ignoreIdconfigurations)
            {
                builder.HasKey(x => x.Id);

                builder.Property(e => e.Id)
                       .ValueGeneratedOnAdd();
            }

            if (typeof(IConcurrencyStamp).GetTypeInfo().IsAssignableFrom(typeof(TEntity)))
            {
                builder
                 .Property(e => ((IConcurrencyStamp)e).ConcurrencyStamp)
                 .IsConcurrencyToken()
                 .HasColumnName(nameof(IConcurrencyStamp.ConcurrencyStamp));
            }


            builder.Ignore(e => e.ObjectState);
            builder.Ignore(e => e.DomainEvents);
        }
    }
}
