using Autofac;
using Castle.DynamicProxy;
using FluentValidation;
using Galaxy.FluentValidation.Bootstrapper.AutoFacModules;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Galaxy.FluentValidation.Bootstrapper
{
   public static class GalaxyFluentValidationRegistrationExtensions
    {

        public static ContainerBuilder UseGalaxyFluentValidation(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            builder.RegisterModule(new FluentValidationModule());

            builder
               .RegisterAssemblyTypes(assemblies)
                 .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                 .AsImplementedInterfaces();

            RegisterFluentInterceptors(builder);
            return builder;
        }

        private static ContainerBuilder RegisterFluentInterceptors(this ContainerBuilder builder)
        {
            builder.RegisterType<ValidatorInterceptor>()
              .AsSelf();
            return builder;
        }

    }
}
