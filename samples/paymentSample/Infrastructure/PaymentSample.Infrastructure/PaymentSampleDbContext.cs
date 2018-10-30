using Galaxy.DataContext;
using Galaxy.EFCore;
using Galaxy.Session;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentSample.Infrastructure
{
    public sealed class PaymentSampleDbContext : GalaxyDbContext, IGalaxyContextAsync
    {
        public const string DEFAULT_SCHEMA = "payment";

        public PaymentSampleDbContext(DbContextOptions options) : base(options)
        {
        }

        public PaymentSampleDbContext(DbContextOptions options, IAppSessionBase appSession) : base(options, appSession)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DEFAULT_SCHEMA);
            base.OnModelCreating(modelBuilder);
        }

    }

}
