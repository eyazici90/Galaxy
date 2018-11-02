using Autofac;
using Galaxy.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.EventStore.Bootstrapper.AutoFacModules
{
    public class UnitOfWorkModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<EventStoreUnitOfWorkAsync>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EventStoreUnitOfWorkAsync>()
               .As<IUnitOfWorkAsync>()
               .InstancePerLifetimeScope();
        }
    }
}
