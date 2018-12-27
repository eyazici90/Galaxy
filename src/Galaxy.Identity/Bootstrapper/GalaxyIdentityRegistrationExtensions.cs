using Autofac;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Identity.Bootstrapper
{
    public static  class GalaxyIdentityRegistrationExtensions
    {
        public static ContainerBuilder UseGalaxyIdentity<TDbContext, TUser, TRole, TPrimaryKey>(
             this ContainerBuilder builder,
             Action<DbContextOptionsBuilder<TDbContext>> optionsAction, Type appSession)
         where TDbContext : GalaxyIdentityDbContext<TUser,TRole,TPrimaryKey>
         where TUser : IdentityUser<TPrimaryKey>
         where TRole : IdentityRole<TPrimaryKey>
         where TPrimaryKey : IEquatable<TPrimaryKey>
        {
            return builder;
        }

        public static ContainerBuilder UseGalaxyIdentity<TDbContext, TUser, TRole, TPrimaryKey>(
            this ContainerBuilder builder,
            Action<DbContextOptionsBuilder<TDbContext>> optionsAction)
         where TDbContext : GalaxyIdentityDbContext<TUser, TRole, TPrimaryKey>
         where TUser : IdentityUser<TPrimaryKey>
         where TRole : IdentityRole<TPrimaryKey>
         where TPrimaryKey : IEquatable<TPrimaryKey>
        {   
            return builder;
        }

        private static void AddGalaxyIdentity<TDbContext, TUser, TRole, TPrimaryKey>(this ContainerBuilder builder,
          Action<DbContextOptionsBuilder<TDbContext>> optionsAction = default)
         where TDbContext : GalaxyIdentityDbContext<TUser, TRole, TPrimaryKey>
         where TUser : IdentityUser<TPrimaryKey>
         where TRole : IdentityRole<TPrimaryKey>
         where TPrimaryKey : IEquatable<TPrimaryKey>
        { 
        }

        private static void AddGalaxyIdentity<TDbContext, TUser, TRole, TPrimaryKey>(this ContainerBuilder builder,
            Action<DbContextOptionsBuilder<TDbContext>> optionsAction = default, Type appSession = default)
         where TDbContext : GalaxyIdentityDbContext<TUser, TRole, TPrimaryKey>
         where TUser : IdentityUser<TPrimaryKey>
         where TRole : IdentityRole<TPrimaryKey>
         where TPrimaryKey : IEquatable<TPrimaryKey>
        {
        }

    }
}
