using Autofac;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Galaxy.FluentValidation.Bootstrapper.AutoFacModules
{
    public class FluentValidationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
              .RegisterAssemblyTypes(typeof(FluentValidationModule).GetTypeInfo().Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();
        }
    }
}
