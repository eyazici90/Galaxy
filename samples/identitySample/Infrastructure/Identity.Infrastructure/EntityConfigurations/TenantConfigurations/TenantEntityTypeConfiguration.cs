using Galaxy.EntityFrameworkCore.EntityConfigurations;
using Identity.Domain.AggregatesModel.TenantAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Infrastructure.EntityConfigurations.TenantConfigurations
{
    
    public class TenantEntityTypeConfiguration
        : GalaxyBaseEntityTypeConfiguration<Tenant>
    {
        public override void Configure(EntityTypeBuilder<Tenant> builder)
        {
            base.Configure(builder);
            builder.ToTable("Tenants");

        }
    }
}
