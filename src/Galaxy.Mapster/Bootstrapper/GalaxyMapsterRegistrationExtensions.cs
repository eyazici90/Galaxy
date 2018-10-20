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
   public static  class GalaxyMapsterRegistrationExtensions
    {

      


        public static ContainerBuilder UseGalaxyMapster(this ContainerBuilder builder, Action initializeMappings = default)
        {
            builder.RegisterModule(new MapsterModule());
            if (initializeMappings != default)
                initializeMappings();
            return builder;
        }

        public static ContainerBuilder UseGalaxyMapster(this ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterModule(new MapsterModule());
            UseAutoMappedTypesAndRegister(assembly);
            return builder;
        }
        public static ContainerBuilder UseGalaxyMapster(this ContainerBuilder builder, Assembly assembly, Action initializeMappings)
        {
            builder.RegisterModule(new MapsterModule());
            UseAutoMappedTypesAndRegister(assembly);
            initializeMappings();
            return builder;
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
