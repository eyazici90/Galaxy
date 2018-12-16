using Autofac;
using Galaxy.Utf8Json.Bootstrapper.AutoFacModules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Utf8Json.Bootstrapper
{
    public static class GalaxyUtf8JsonRegistrationExtensions
    { 
        public static ContainerBuilder UseGalaxyUtf8JsonSerialization(this ContainerBuilder builder)
        {
            RegisterUtf8JsonModule(builder);
            return builder;
        }

        public static ContainerBuilder UseGalaxyUtf8JsonSerialization(this ContainerBuilder builder, Action<ContainerBuilder> containerAction)
        {
            RegisterUtf8JsonModule(builder);
            containerAction(builder);
            return builder;
        }

        private static ContainerBuilder RegisterUtf8JsonModule(ContainerBuilder builder)
        {
            builder.RegisterModule(new Utf8JsonSerializerModule());
            return builder;
        }
    }
}
