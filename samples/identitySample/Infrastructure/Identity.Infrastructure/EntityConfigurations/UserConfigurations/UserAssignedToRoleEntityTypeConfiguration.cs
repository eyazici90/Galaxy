using Galaxy.EntityFrameworkCore.EntityConfigurations;
using Identity.Domain.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Infrastructure.EntityConfigurations.UserConfigurations
{

    public class UserAssignedToRoleEntityTypeConfiguration : IEntityTypeConfiguration<UserAssignedToRole>
    {
        public void Configure(EntityTypeBuilder<UserAssignedToRole> builder)
        {

            //builder.ToTable("UserRoles");

            builder.HasIndex(e => new { e.UserId, e.RoleId });

            builder.Ignore(e => e.ObjectState);
            builder.Ignore(e => e.DomainEvents);

            builder.Property(e => e.RoleId).HasColumnName("RoleId");
            builder.Property(e => e.UserId).HasColumnName("UserId");

        }
    }
}
