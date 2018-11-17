using Galaxy.EntityFrameworkCore.EntityConfigurations;
using Identity.Domain.AggregatesModel.PermissionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Infrastructure.EntityConfigurations.PermissionConfigurations
{
    public class PermissionEntityTypeConfiguration : GalaxyBaseAuditEntityTypeConfiguration<Permission>
    {
        public override void Configure(EntityTypeBuilder<Permission> builder)
        {
            base.Configure(builder);
            builder.ToTable("Permissions");

            builder.HasKey(x => x.Id);

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();
        }
    }
}
