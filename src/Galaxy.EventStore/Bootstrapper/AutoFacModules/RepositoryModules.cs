using Autofac;
using Galaxy.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.EventStore.Bootstrapper.AutoFacModules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(AggregateRootRepository<>))
               .As(typeof(IRepository<>))
               .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(AggregateRootRepository<>))
               .As(typeof(IRepositoryAsync<>))
               .InstancePerLifetimeScope();


            builder.RegisterGeneric(typeof(AggregateRootRepository<,>))
             .As(typeof(IRepository<,>))
             .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(AggregateRootRepository<,>))
              .As(typeof(IRepositoryAsync<,>))
              .InstancePerLifetimeScope();


        }
    }
}
