using Identity.Domain.AggregatesModel.TenantAggregate;
using Identity.Domain.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Infrastructure.EntityConfigurations.UserConfigurations
{

    public class UserEntityTypeConfiguration
            : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Ignore(e => e.ObjectState);
            builder.Ignore(e => e.DomainEvents);

            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();


            builder.HasMany(e => e.UserRoles)
                .WithOne()
                .HasForeignKey(e => e.UserId);

            builder.HasMany(e => e.UserPermissions)
                .WithOne()
                .HasForeignKey(e => e.UserId);

            builder.HasOne<Tenant>()
                .WithMany()
                .HasForeignKey(e => e.TenantId);
        }
    }
}
