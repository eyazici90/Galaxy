using CustomerSample.Infrastructure.EntityConfigurations;
using Galaxy.DataContext;
using Galaxy.EFCore;
using Galaxy.EFCore.Extensions;
using Galaxy.Session;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace CustomerSample.Infrastructure
{
    public sealed class CustomerSampleDbContext : GalaxyDbContext, IGalaxyContextAsync
    {
        public const string DEFAULT_SCHEMA = "customer";

        public CustomerSampleDbContext(DbContextOptions options) : base(options)
        {
        }

        public CustomerSampleDbContext(DbContextOptions options, IAppSessionBase appSession) : base(options, appSession)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DEFAULT_SCHEMA);
            base.OnModelCreating(modelBuilder);
        }
     
    }

   
}
