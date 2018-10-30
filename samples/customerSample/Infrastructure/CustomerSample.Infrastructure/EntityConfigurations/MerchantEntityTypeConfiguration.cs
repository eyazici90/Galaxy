using CustomerSample.Customer.Domain.AggregatesModel.BrandAggregate;
using Galaxy.EntityFrameworkCore.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CustomerSample.Infrastructure.EntityConfigurations
{
    public class MerchantEntityTypeConfiguration : GalaxyBaseEntityTypeConfiguration<Merchant>
    {
        public override void Configure(EntityTypeBuilder<Merchant> builder)
        {
            base.Configure(builder);

            builder.ToTable("Merchants");
        
            builder.Property(e => e.Name)
               .HasColumnType("nvarchar(40)")
               .IsRequired(true);

            builder.Property(e => e.Surname)
               .HasColumnType("nvarchar(50)")
               .IsRequired(true);

            builder.Property(e => e.Vkn)
                .HasColumnType("nvarchar(11)")
                .IsRequired(false);

            builder.Property(e => e.Gsm)
                .HasColumnType("nvarchar(25)")
                .IsRequired(false);

            

        }
    }
}
