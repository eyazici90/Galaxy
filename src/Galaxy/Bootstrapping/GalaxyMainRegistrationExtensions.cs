using Autofac;
using Castle.DynamicProxy;
using Galaxy.Application;
using Galaxy.Bootstrapping.AutoFacModules;
using Galaxy.Domain;
using Galaxy.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Galaxy.Bootstrapping
{
   public static class GalaxyMainRegistrationExtensions
    {
        
        public static IContainer InitializeGalaxy(this ContainerBuilder builder)
        {
            GalaxyCoreModule.SingleInstanceBuilder = builder;
            GalaxyCoreModule.Container = builder.Build();
            return GalaxyCoreModule.Container;
        }
        

        public static ContainerBuilder UseGalaxyCore(this ContainerBuilder builder, params Assembly[] assembliesForInterceptors) 
        {
            if (assembliesForInterceptors != null)
                RegisterInterceptorsIfAnyExist(builder, assembliesForInterceptors);
            
            return builder;
        }


        public static ContainerBuilder UseGalaxyCore(this ContainerBuilder builder, Action<ContainerBuilder> action, params Assembly[] assembliesForInterceptors)
        {
            action(builder);

            if (assembliesForInterceptors != null)
                RegisterInterceptorsIfAnyExist(builder, assembliesForInterceptors);
            return builder;
        }

        private static ContainerBuilder RegisterInterceptorsIfAnyExist(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            builder.RegisterAssemblyTypes(assemblies)
             .AssignableTo<IInterceptor>()
             .AsSelf();
            return builder;
        }


        public static ContainerBuilder UseConventionalCustomRepositories(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            builder.RegisterAssemblyTypes(assemblies)
              .AssignableTo<ICustomRepository>()
              .AsImplementedInterfaces()
              .InstancePerDependency();

            return builder;
        }

        public static ContainerBuilder UseConventionalPolicies(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            builder.RegisterAssemblyTypes(assemblies)
              .AssignableTo<IPolicy>()
              .AsImplementedInterfaces()
              .InstancePerDependency();

            return builder;
        }

        public static ContainerBuilder UseConventionalDomainService(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            builder.RegisterAssemblyTypes(assemblies)
              .AssignableTo<IDomainService>()
              .AsImplementedInterfaces()
              .InstancePerDependency();

            return builder;
        }

        public static ContainerBuilder UseConventionalDomainEventHandlers(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            builder.RegisterAssemblyTypes(assemblies)
                 .AsClosedTypesOf(typeof(INotificationHandler<>));
            return builder;
        }

        public static ContainerBuilder UseConventionalCommandHandlers(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            builder.RegisterAssemblyTypes(assemblies)
                 .AsClosedTypesOf(typeof(IRequestHandler<,>));
            return builder;
        }

        public static ContainerBuilder UseConventionalApplicationService(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            builder.RegisterAssemblyTypes(assemblies)
              .AssignableTo<IApplicationService>()
              .AsImplementedInterfaces()
              .InstancePerDependency();

            return builder;
        }

    }
}
