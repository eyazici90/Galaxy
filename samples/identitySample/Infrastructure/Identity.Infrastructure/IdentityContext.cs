using Galaxy.DataContext;
using Galaxy.Identity;
using Galaxy.Session;
using Identity.Domain.AggregatesModel.RoleAggregate;
using Identity.Domain.AggregatesModel.UserAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Infrastructure
{
    public sealed class IdentityContext : GalaxyDbContext<User, Role, int>, IGalaxyContextAsync
    {
        public const string DEFAULT_SCHEMA = "identity";

        public IdentityContext(DbContextOptions options) : base(options)
        {
        }

        public IdentityContext(DbContextOptions options, IAppSessionBase appSession) : base(options, appSession)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DEFAULT_SCHEMA);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims", DEFAULT_SCHEMA);
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins", DEFAULT_SCHEMA);
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRoles", DEFAULT_SCHEMA);
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims", DEFAULT_SCHEMA);
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UserTokens", DEFAULT_SCHEMA);
        }

    }
}
