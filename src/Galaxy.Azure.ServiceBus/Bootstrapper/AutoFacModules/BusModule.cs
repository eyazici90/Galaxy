using Autofac;
using Galaxy.Commands;
using Galaxy.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Azure.ServiceBus.Bootstrapper.AutoFacModules
{
    public class BusModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GalaxyAzureServiceBus>()
                 .As<IEventBus>()
                 .InstancePerDependency();

            builder.RegisterType<GalaxyAzureServiceBus>()
                 .As<ICommandBus>()
                 .InstancePerDependency();
        }

    }
}