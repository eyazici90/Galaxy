using CustomerSample.Domain.AggregatesModel.LimitationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerSample.Infrastructure.EntityConfigurations
{
    public class LimitTypeEntityConfiguration : IEntityTypeConfiguration<LimitType>
    {
        public void Configure(EntityTypeBuilder<LimitType> builder)
        {
            builder.ToTable("LimitTypes");
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();

            builder.Ignore(e => e.ObjectState);
        }
    }
}
