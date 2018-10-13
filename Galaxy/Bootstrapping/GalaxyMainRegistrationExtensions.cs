using Autofac;
using Galaxy.Application;
using Galaxy.Bootstrapping.AutoFacModules;
using Galaxy.Domain;
using Galaxy.Repositories;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Galaxy.Bootstrapping
{
   public static class GalaxyMainRegistrationExtensions
    {
        
        public static ContainerBuilder UseGalaxyCore(this ContainerBuilder builder) 
        {
            builder.RegisterModule(new MediatrModule());
            return builder;
        }

        public static ContainerBuilder UseConventinalCustomRepositories(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            builder.RegisterAssemblyTypes(assemblies)
              .AssignableTo<ICustomRepository>()
              .AsImplementedInterfaces()
              .InstancePerDependency();

            return builder;
        }

        public static ContainerBuilder UseConventinalDomainService(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            builder.RegisterAssemblyTypes(assemblies)
              .AssignableTo<IDomainService>()
              .AsImplementedInterfaces()
              .InstancePerDependency();

            return builder;
        }

        public static ContainerBuilder UseConventinalApplicationService(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            builder.RegisterAssemblyTypes(assemblies)
              .AssignableTo<IApplicationService>()
              .AsImplementedInterfaces()
              .InstancePerDependency();

            return builder;
        }

    }
}
