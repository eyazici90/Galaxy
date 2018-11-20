using Autofac;
using Galaxy.RabbitMQ.Bootstrapper.AutoFacModules;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Galaxy.RabbitMQ.Bootstrapper
{
   public static class GalaxyRabbitMQRegistrationExtensions
    {
        public static ContainerBuilder UseGalaxyRabbitMQ(this ContainerBuilder builder, Action<IGalaxyRabbitMQConfiguration> galaxyRabbitMQConfiguration)
        {
            
            builder.Register(c =>
            {
                var configs = new GalaxyRabbitMQConfiguration();
                galaxyRabbitMQConfiguration(configs);
                return configs;
            })
            .As<IGalaxyRabbitMQConfiguration>()
            .SingleInstance();

       
            builder.RegisterModule(new MassTransitModule());
            builder.RegisterModule(new BusModules());


            return builder;
        }

        public static ContainerBuilder UseGalaxyRabbitMQ(this ContainerBuilder builder, Action<IGalaxyRabbitMQConfiguration> galaxyRabbitMQConfiguration
            , params Assembly[] consumersAssembly)
        {
            UseGalaxyRabbitMQ(builder, galaxyRabbitMQConfiguration);
            builder.RegisterConsumers(consumersAssembly);
            return builder;
        }

    }
}
