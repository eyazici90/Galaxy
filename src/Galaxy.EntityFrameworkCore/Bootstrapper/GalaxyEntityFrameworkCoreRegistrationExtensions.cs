using Autofac;
using Galaxy.DataContext;
using Galaxy.EFCore;
using Galaxy.EntityFrameworkCore.Bootstrapper.AutoFacModules;
using Galaxy.Exceptions;
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
           Action<DbContextOptionsBuilder<TDbContext>> optionsAction, Type appSession ) 
            where TDbContext : GalaxyDbContext
        {

            if (!typeof(IAppSessionBase).IsAssignableFrom(appSession))
                throw new GalaxyException($"The parameter : {appSession.Name} is not assignable from {nameof(IAppSessionBase)}");
            
            builder.RegisterType(appSession)
                .As<IAppSessionBase>()
                .InstancePerLifetimeScope();
         
            builder.AddGalaxyDbContext<TDbContext>(optionsAction, appSession );

            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new UnitOfWorkModule());

            return builder;
        }

        public static ContainerBuilder UseGalaxyEntityFrameworkCore<TDbContext>(
            this ContainerBuilder builder,
            Action<DbContextOptionsBuilder<TDbContext>> optionsAction) where TDbContext : GalaxyDbContext
        {
            builder.RegisterType<SessionBase>()
                .As<IAppSessionBase>()
                .InstancePerLifetimeScope();
            
            builder.AddGalaxyDbContext<TDbContext>(optionsAction);

            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new UnitOfWorkModule());

            return builder;
        }

        private static void AddGalaxyDbContext<TDbContext>(this ContainerBuilder builder,
          Action<DbContextOptionsBuilder<TDbContext>> optionsAction = default)
          where TDbContext : GalaxyDbContext
        {
            builder.AddGalaxyDbContext(optionsAction, typeof(SessionBase));
        }

        private static void AddGalaxyDbContext<TDbContext>(this ContainerBuilder builder,
            Action<DbContextOptionsBuilder<TDbContext>>   optionsAction = default, Type appSession = default)
            where TDbContext : GalaxyDbContext
        {
            var options = new DbContextOptionsBuilder<TDbContext>();
            optionsAction(options);

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
