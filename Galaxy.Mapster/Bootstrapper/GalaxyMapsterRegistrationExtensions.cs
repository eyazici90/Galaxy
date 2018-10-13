using Autofac;
using Galaxy.Mapster.Bootstrapper.AutoFacModules;
using System;
using System.Collections.Generic;
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
    }
}
