using Autofac;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Identity.Bootstrapper
{
    public static  class GalaxyIdentityRegistrationExtensions
    {
        public static ContainerBuilder UseGalaxyIdentity<TDbContext, TUser, TRole, TPrimaryKey>(this ContainerBuilder builder, IServiceCollection services,
             Action<IdentityOptions> optionsAction)
         where TDbContext : GalaxyIdentityDbContext<TUser, TRole, TPrimaryKey>
         where TUser : IdentityUser<TPrimaryKey>
         where TRole : IdentityRole<TPrimaryKey>
         where TPrimaryKey : IEquatable<TPrimaryKey>
        {
            AddGalaxyIdentity<TDbContext, TUser, TRole, TPrimaryKey>(builder, services, optionsAction);
            return builder;
        }

        private static void AddGalaxyIdentity<TDbContext, TUser, TRole, TPrimaryKey>(this ContainerBuilder builder, IServiceCollection services,
           Action<IdentityOptions> optionsAction)
         where TDbContext : GalaxyIdentityDbContext<TUser, TRole, TPrimaryKey>
         where TUser : IdentityUser<TPrimaryKey>
         where TRole : IdentityRole<TPrimaryKey>
         where TPrimaryKey : IEquatable<TPrimaryKey>
        { 
            services.AddIdentity<TUser, TRole>(opt =>
            {
                optionsAction(opt);
            })
           .AddEntityFrameworkStores<TDbContext>();
        }

    }
}
