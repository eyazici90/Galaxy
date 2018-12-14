using Autofac;
using Galaxy.NewtonSoftJson.Bootstrapper.AutoFacModules;
using System;

namespace Galaxy.NewtonSoftJson.Bootstrapper
{
    public static class GalaxyNewtonSoftJsonRegistrationExtensions
    {
        public static ContainerBuilder UseGalaxyNewtonSoftJsonSerialization(this ContainerBuilder builder)
        {
            RegisterNewtonSoftModule(builder);
            return builder;
        }

        public static ContainerBuilder UseGalaxyNewtonSoftJsonSerialization(this ContainerBuilder builder, Action<ContainerBuilder> containerAction)
        {
            RegisterNewtonSoftModule(builder);
            containerAction(builder);
            return builder;
        }

        private static ContainerBuilder RegisterNewtonSoftModule(ContainerBuilder builder)
        {
            builder.RegisterModule(new NewtonSoftSerializerModule());
            return builder;
        }
    }
}
