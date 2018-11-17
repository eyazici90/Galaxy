using Galaxy.EntityFrameworkCore.EntityConfigurations;
using Identity.Domain.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Infrastructure.EntityConfigurations.UserConfigurations
{
    public class UserAssignedToPermissionEntityTypeConfiguration
           : GalaxyBaseEntityTypeConfiguration<UserAssignedToPermission>
    {
        public override void Configure(EntityTypeBuilder<UserAssignedToPermission> builder)
        {

            base.Configure(builder);

            builder.Property<int>("Id")
            .IsRequired()
            .ValueGeneratedOnAdd();

            builder.HasKey("Id");
            builder.ToTable("UserPermissions");

            builder.HasIndex(e => new { e.UserId, e.PermissionId });

            builder.Property(e => e.PermissionId).HasColumnName("PermissionId");
            builder.Property(e => e.UserId).HasColumnName("UserId");

        }
    }
}
