using Autofac;
using Galaxy.RabbitMQ.Bootstrapper.AutoFacModules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.RabbitMQ.Bootstrapper
{
   public static class GalaxyRabbitMQRegistrationExtensions
    {
        public static ContainerBuilder UseGalaxyRabbitMQ(this ContainerBuilder builder, Func<IGalaxyRabbitMQConfiguration , IGalaxyRabbitMQConfiguration> galaxyRabbitMQConfiguration)
        {
            builder.Register(ctx => galaxyRabbitMQConfiguration)
                .AsSelf();
            builder.RegisterModule(new ConfigurationModule());
            builder.RegisterModule(new MassTransitModule());
            return builder;
        }
        
    }
}
