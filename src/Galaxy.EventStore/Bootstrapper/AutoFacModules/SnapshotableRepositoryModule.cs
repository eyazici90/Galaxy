using Autofac;
using Galaxy.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.EventStore.Bootstrapper.AutoFacModules
{ 
    public class SnapshotableRepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(SnapshotableRepository<>))
               .As(typeof(IRepository<>))
               .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(SnapshotableRepository<>))
               .As(typeof(IRepositoryAsync<>))
               .InstancePerLifetimeScope();


            builder.RegisterGeneric(typeof(SnapshotableRepository<,>))
             .As(typeof(IRepository<,>))
             .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(SnapshotableRepository<,>))
              .As(typeof(IRepositoryAsync<,>))
              .InstancePerLifetimeScope();
        }
    }
}
