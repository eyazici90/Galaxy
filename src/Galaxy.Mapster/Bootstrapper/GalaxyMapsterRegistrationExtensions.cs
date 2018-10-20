using Autofac;
using Galaxy.Mapster.Bootstrapper.AutoFacModules;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Galaxy.Mapster.Bootstrapper
{
   public static class GalaxyMapsterRegistrationExtensions
    {
        private static void ConfigureAdepterConfigs(List<Action<TypeAdapterConfig>> listofConfigurations) =>
           listofConfigurations.ForEach(conf => conf(GetGlobalTypeAdepterConfig));

        private static TypeAdapterConfig GetGlobalTypeAdepterConfig => TypeAdapterConfig.GlobalSettings;

        private static ContainerBuilder UseGalaxyMapster(this ContainerBuilder builder)
        {
            builder.RegisterModule(new MapsterModule());
            return builder;
        }

        public static ContainerBuilder UseGalaxyMapster(this ContainerBuilder builder, List<Action<TypeAdapterConfig>>  configurations = default)
        {
            if (configurations != default)
                ConfigureAdepterConfigs(configurations);
            return UseGalaxyMapster(builder); 
        }

        public static ContainerBuilder UseGalaxyMapster(this ContainerBuilder builder, Assembly assembly)
        {
            UseAutoMappedTypesAndRegister(assembly);
            return UseGalaxyMapster(builder); 
        }

        public static ContainerBuilder UseGalaxyMapster(this ContainerBuilder builder, Assembly assembly, List<Action<TypeAdapterConfig>> configurations)
        {
            UseAutoMappedTypesAndRegister(assembly);
            ConfigureAdepterConfigs(configurations);
            return UseGalaxyMapster(builder);
        }
        

        private static TypeAdapterConfig UseAutoMappedTypesAndRegister(Assembly assembly)
        {
            Type[] types = assembly.GetTypes().Where(type =>
                 type.GetTypeInfo().IsDefined(typeof(AutoMapAttribute))
             ).ToArray();

            var configurations = new TypeAdapterConfig();
            foreach (Type type in types)
            {
                configurations.CreateAutoAttributeMaps(type);
            }
            return configurations;
        }

    }
}
