using Autofac;
using Galaxy.AutoMapper.Bootstrapper.AutoFacModules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.AutoMapper.Bootstrapper
{
   public static  class GalaxyAutoMapperRegistrationExtensions
    {
        public static ContainerBuilder UseGalaxyAutoMapper(this ContainerBuilder builder)
        {
            builder.RegisterModule(new AutoMapperModule());
            return builder;
        }
        public static ContainerBuilder UseGalaxyAutoMapper(this ContainerBuilder builder, Action initializeMappings = default)
        {
            UseGalaxyAutoMapper(builder);
            if (initializeMappings != default)
                initializeMappings();
            return builder;
        }
    }

}
