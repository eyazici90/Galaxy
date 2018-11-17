using Galaxy.EntityFrameworkCore.EntityConfigurations;
using Identity.Domain.AggregatesModel.RoleAggregate;
using Identity.Domain.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Infrastructure.EntityConfigurations.RoleConfigurations
{
    public class RoleEntityTypeConfiguration
          : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.Ignore(e => e.ObjectState);

            builder.HasKey(x => x.Id);

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();


            builder.HasMany<UserAssignedToRole>()
                .WithOne()
                .HasForeignKey(e => e.RoleId);

        }
    }
}
