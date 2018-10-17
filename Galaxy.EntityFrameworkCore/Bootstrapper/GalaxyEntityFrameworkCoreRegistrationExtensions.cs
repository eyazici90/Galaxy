using Autofac;
using Galaxy.DataContext;
using Galaxy.EFCore;
using Galaxy.EntityFrameworkCore.Bootstrapper.AutoFacModules;
using Galaxy.Session;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.EntityFrameworkCore.Bootstrapper
{
   public static class GalaxyEntityFrameworkCoreRegistrationExtensions
    {

        public static ContainerBuilder UseGalaxyEntityFrameworkCore<TDbContext, TAppSession>(
           this ContainerBuilder builder,
           DbContextOptionsBuilder<TDbContext> options, TAppSession appSession ) 
            where TDbContext : GalaxyDbContext
            where TAppSession : Type
        {
            
            builder.RegisterType(appSession)
                .As<IAppSessionBase>()
                .InstancePerLifetimeScope();
         
            builder.AddGalaxyDbContext<TDbContext,TAppSession>(options, appSession );

            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new UnitOfWorkModule());

            return builder;
        }

        public static ContainerBuilder UseGalaxyEntityFrameworkCore<TDbContext>(
            this ContainerBuilder builder,
            DbContextOptionsBuilder<TDbContext> options) where TDbContext : GalaxyDbContext
        {
            builder.RegisterType<SessionBase>()
                .As<IAppSessionBase>()
                .InstancePerLifetimeScope();
            
            builder.AddGalaxyDbContext<TDbContext>(options);

            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new UnitOfWorkModule());

            return builder;
        }

        private static void AddGalaxyDbContext<TDbContext>(this ContainerBuilder builder,
           DbContextOptionsBuilder<TDbContext> options = default)
          where TDbContext : GalaxyDbContext
        {
            builder.AddGalaxyDbContext(options, typeof(SessionBase));
        }

        private static void AddGalaxyDbContext<TDbContext,TAppSession>(this ContainerBuilder builder,
             DbContextOptionsBuilder<TDbContext> options = default, TAppSession appSession = default)
            where TDbContext : GalaxyDbContext
            where TAppSession : Type
        {

            builder.Register(c =>
            {
                return options.Options;
            })
           .AsSelf()
           .SingleInstance();

            builder.RegisterType<TDbContext>()
            .AsSelf()
            .UsingConstructor(typeof(DbContextOptions), typeof(IAppSessionBase))
            .WithParameters(new[]
            {
                new NamedParameter(nameof(GalaxyDbConnectionParameters.options), options.Options ),
                new NamedParameter(nameof(GalaxyDbConnectionParameters.appSession), Activator.CreateInstance(appSession))
            })
            .InstancePerLifetimeScope();

            builder.RegisterType<TDbContext>()
               .As<IGalaxyContextAsync>()
               .UsingConstructor(typeof(DbContextOptions), typeof(IAppSessionBase))
               .WithParameters(new[]
               {
                   new NamedParameter(nameof(GalaxyDbConnectionParameters.options), options.Options ),
                   new NamedParameter(nameof(GalaxyDbConnectionParameters.appSession),Activator.CreateInstance(appSession))
               })
               .InstancePerLifetimeScope();

        }


    }
}
