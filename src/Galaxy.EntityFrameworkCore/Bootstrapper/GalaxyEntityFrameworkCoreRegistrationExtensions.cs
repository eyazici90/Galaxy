using Autofac;
using Autofac.Core;
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
                .As<IAppSessionContext>()
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
                .As<IAppSessionContext>()
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

            if (!typeof(IAppSessionContext).IsAssignableFrom(appSession))
                throw new GalaxyException($"The parameter : {appSession.Name} is not assignable from {typeof(IAppSessionContext).Name}");

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
                .UsingConstructor(typeof(DbContextOptions), typeof(IAppSessionContext))
                .WithParameters(new[]
                {
                    new ResolvedParameter(
                       (pi, ctx) => pi.ParameterType == typeof(DbContextOptions) ,
                       (pi, ctx) => options.Options),

                     new ResolvedParameter(
                       (pi, ctx) => pi.ParameterType == typeof(IAppSessionContext) ,
                       (pi, ctx) => ctx.Resolve<IAppSessionContext>())
                       
                })
                .InstancePerLifetimeScope();
            
            builder.RegisterType<TDbContext>()
               .As<IGalaxyContextAsync>()
               .UsingConstructor(typeof(DbContextOptions), typeof(IAppSessionContext))
               .WithParameters(new[]
               {
                     new ResolvedParameter(
                       (pi, ctx) => pi.ParameterType == typeof(DbContextOptions) ,
                       (pi, ctx) => options.Options),

                     new ResolvedParameter(
                       (pi, ctx) => pi.ParameterType == typeof(IAppSessionContext) ,
                       (pi, ctx) => ctx.Resolve<IAppSessionContext>())
               })
               .InstancePerLifetimeScope(); 
        } 

    }
}
