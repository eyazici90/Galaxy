using CustomerSample.Customer.Domain.AggregatesModel.GroupAggregate;
using Galaxy.EntityFrameworkCore.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CustomerSample.Infrastructure.EntityConfigurations
{
    public class GroupEntityTypeConfiguration : GalaxyBaseEntityTypeConfiguration<Group, Guid>
    {
        public override void Configure(EntityTypeBuilder<Group> builder)
        {
            base.Configure(builder);

            builder.ToTable("Groups");
          
        }
    }
}
