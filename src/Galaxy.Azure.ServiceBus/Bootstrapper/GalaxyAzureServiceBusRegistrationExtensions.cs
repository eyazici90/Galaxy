using Autofac;
using Galaxy.Azure.ServiceBus.Bootstrapper.AutoFacModules;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Galaxy.Azure.ServiceBus.Bootstrapper
{
    public static class GalaxyAzureServiceBusRegistrationExtensions
    {
        public static ContainerBuilder UseGalaxyAzureServiceBus(this ContainerBuilder builder, Action<IGalaxyAzureServiceBusConfigurations> galaxyAzureServiceBusConfiguration)
        { 
            builder.Register(c =>
            {
                var configs = new GalaxyAzureServiceBusConfigurations();
                galaxyAzureServiceBusConfiguration(configs);
                return configs;
            })
            .As<IGalaxyAzureServiceBusConfigurations>()
            .SingleInstance();


            builder.RegisterAssemblyModules(typeof(BusModules).Assembly);

            return builder;
        }
 
    }
}
