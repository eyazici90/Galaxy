using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.RabbitMQ.Bootstrapper.AutoFacModules
{
    
    public class ConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GalaxyRabbitMQConfiguration>()
                 .As<IGalaxyRabbitMQConfiguration>()
                 .SingleInstance();
        }
    }
}
