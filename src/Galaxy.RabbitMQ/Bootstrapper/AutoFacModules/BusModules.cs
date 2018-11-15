using Autofac;
using Galaxy.Bootstrapping;
using Galaxy.Commands;
using Galaxy.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.RabbitMQ.Bootstrapper.AutoFacModules
{
    public class BusModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GalaxyEventBusRabbitMQ>()
                 .As<IEventBus>()
                 .InstancePerDependency();

            builder.RegisterType<GalaxyCommandBusRabbitMQ>()
                 .As<ICommandBus>()
                 .InstancePerDependency();
        }
    }
}
