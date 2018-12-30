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
        public static ContainerBuilder UseGalaxyEntityFrameworkCore<TDbContext>(this ContainerBuilder builder,
           Action<DbContextOptionsBuilder<TDbContext>> optionsAction) where TDbContext : DbContext
        {
            builder.RegisterType<SessionBase>()
                .As<IAppSessionBase>()
                .InstancePerLifetimeScope();

            builder.AddGalaxyDbContext<TDbContext>(optionsAction);

            builder.RegisterAssemblyModules(typeof(RepositoryModule).Assembly);

            return builder;
        }

        public static ContainerBuilder UseGalaxyEntityFrameworkCore<TDbContext>(this ContainerBuilder builder,
            Action<DbContextOptionsBuilder<TDbContext>> optionsAction, Type appSession ) 
            where TDbContext : DbContext
        { 
            builder.RegisterType(appSession)
                .As<IAppSessionBase>()
                .InstancePerLifetimeScope();
         
            builder.AddGalaxyDbContext<TDbContext>(optionsAction, appSession );

            builder.RegisterAssemblyModules(typeof(RepositoryModule).Assembly);
            
            return builder;
        }

        private static void AddGalaxyDbContext<TDbContext>(this ContainerBuilder builder,
          Action<DbContextOptionsBuilder<TDbContext>> optionsAction)
          where TDbContext : DbContext
        {
            builder.AddGalaxyDbContext(optionsAction, typeof(SessionBase));
        }

        private static void AddGalaxyDbContext<TDbContext>(this ContainerBuilder builder,
            Action<DbContextOptionsBuilder<TDbContext>>   optionsAction = default, Type appSession = default)
            where TDbContext : DbContext
        {
            if (!typeof(IGalaxyContextAsync).IsAssignableFrom(typeof(TDbContext)))
                throw new GalaxyException($"The parameter : {typeof(TDbContext).Name} is not assignable from {typeof(IGalaxyContextAsync).Name}");

            if (!typeof(IAppSessionBase).IsAssignableFrom(appSession))
                throw new GalaxyException($"The parameter : {appSession.Name} is not assignable from {typeof(IAppSessionBase).Name}");

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
