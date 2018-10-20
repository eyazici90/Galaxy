using CustomerSample.Customer.Domain.AggregatesModel.BrandAggregate;
using Galaxy.EntityFrameworkCore.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CustomerSample.Infrastructure.EntityConfigurations
{
    public class BrandEntityTypeConfiguration : GalaxyBaseAuditEntityTypeConfiguration<Brand>
    {
        public override void Configure(EntityTypeBuilder<Brand> builder)
        {
            base.Configure(builder);
            builder.ToTable("Brands");
      
            
            builder.Property(e => e.BrandName)
               .HasColumnType("nvarchar(40)")
               .IsRequired(true);

            builder.Property(e => e.EMail)
               .HasColumnType("nvarchar(30)")
               .IsRequired(true);

            builder.Property(e => e.Gsm)
               .IsRequired(false);


            builder.HasMany(e => e.Merchants)
                .WithOne()
                .HasForeignKey(e => e.BrandId);
                

        }
    }
}
