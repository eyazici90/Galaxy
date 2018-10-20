using CustomerSample.Customer.Domain.AggregatesModel.BrandAggregate;
using CustomerSample.Domain.AggregatesModel.LimitationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerSample.Infrastructure.EntityConfigurations
{
    class LimitEntityTypeConfiguration : IEntityTypeConfiguration<Limit>
    {
        public void Configure(EntityTypeBuilder<Limit> builder)
        {

            builder.ToTable("Limits");
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();

            builder.Ignore(e => e.ObjectState);

            builder.Property(e => e.LimitName)
               .HasColumnType("nvarchar(40)")
               .IsRequired(true);

            builder.HasOne(e => e.LimitType)
                .WithMany()
                .HasForeignKey(e => e.LimitTypeId);


            // Different Aggragetes Relations
            builder.HasMany<Merchant>()
                .WithOne()
                .HasForeignKey(e=>e.LimitId);



        }
    }
}
