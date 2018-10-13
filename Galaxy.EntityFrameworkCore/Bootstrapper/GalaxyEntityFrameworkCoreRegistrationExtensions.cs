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
        public static ContainerBuilder UseGalaxyEntityFrameworkCore<TDbContext>(
            this ContainerBuilder builder,
            DbContextOptionsBuilder<TDbContext> options , IAppSessionBase appSession = default) where TDbContext : GalaxyDbContext
        {
            if (appSession != default)
            {

                builder.RegisterInstance(appSession.GetType())
                .As<IAppSessionBase>()
                .InstancePerLifetimeScope();
            }
            else
            {
             
                builder.RegisterType<SessionBase>()
                .As<IAppSessionBase>()
                .InstancePerLifetimeScope();
            }

            builder.AddGalaxyDbContext<TDbContext>(options, appSession != default ? appSession : new SessionBase());

            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new UnitOfWorkModule());

            return builder;
        }

        private static void AddGalaxyDbContext<TDbContext>(this ContainerBuilder builder,
             DbContextOptionsBuilder<TDbContext> options = default, IAppSessionBase appSession = default) where TDbContext : GalaxyDbContext 
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
                new NamedParameter(nameof(GalaxyDbConnectionParameters.appSession),  appSession)
            })
            .InstancePerLifetimeScope();

            builder.RegisterType<TDbContext>()
               .As<IGalaxyContextAsync>()
               .UsingConstructor(typeof(DbContextOptions), typeof(IAppSessionBase))
               .WithParameters(new[]
               {
                   new NamedParameter(nameof(GalaxyDbConnectionParameters.options), options.Options ),
                   new NamedParameter(nameof(GalaxyDbConnectionParameters.appSession),  appSession)
               })
               .InstancePerLifetimeScope();

        }

    }
}
