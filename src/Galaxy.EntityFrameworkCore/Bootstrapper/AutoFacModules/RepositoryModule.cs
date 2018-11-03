using Autofac;
using Galaxy.EFCore;
using Galaxy.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.EntityFrameworkCore.Bootstrapper.AutoFacModules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EFRepository<>))
               .As(typeof(IRepository<>))
               .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EFRepository<>))
               .As(typeof(IRepositoryAsync<>))
               .InstancePerLifetimeScope();


            builder.RegisterGeneric(typeof(EFRepository<,>))
             .As(typeof(IRepository<,>))
             .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EFRepository<,>))
              .As(typeof(IRepositoryAsync<,>))
              .InstancePerLifetimeScope();


        }
    }
}
