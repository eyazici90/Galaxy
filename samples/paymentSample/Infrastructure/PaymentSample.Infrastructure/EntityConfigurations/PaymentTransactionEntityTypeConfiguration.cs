using Galaxy.EntityFrameworkCore.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentSample.Domain.AggregatesModel.PaymentAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentSample.Infrastructure.EntityConfigurations
{
    public class PaymentTransactionEntityTypeConfiguration : GalaxyBaseEntityTypeConfiguration<PaymentTransaction,Guid>
    {
        public override void Configure(EntityTypeBuilder<PaymentTransaction> builder)
        {
            base.Configure(builder);

            builder.ToTable("PaymentTransactions");


            builder.OwnsOne(e => e.Money)
            .Property(e => e.Amount)
            .HasColumnName("Amount");

            builder.OwnsOne(e => e.Money)
                .Property(e => e.CurrencyCode)
                .HasColumnName("CurrencyCode");

            builder.HasOne(e => e.PaymentTransactionType)
                .WithMany()
                .HasForeignKey(e => e.TransactionTypeId);

        }
    }
}
