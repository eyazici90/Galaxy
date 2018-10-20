using CustomerSample.Customer.Domain.AggregatesModel.BrandAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CustomerSample.Infrastructure.EntityConfigurations
{
    public class MerchantEntityTypeConfiguration : IEntityTypeConfiguration<Merchant>
    {
        public void Configure(EntityTypeBuilder<Merchant> builder)
        {
            builder.ToTable("Merchants");
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();

            builder.Ignore(e => e.ObjectState);

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
