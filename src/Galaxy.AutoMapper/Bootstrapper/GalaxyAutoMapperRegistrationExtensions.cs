using Autofac;
using AutoMapper;
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
        public static ContainerBuilder UseGalaxyAutoMapper(this ContainerBuilder builder, Action<IMapperConfigurationExpression> configurations = default)
        {
            UseGalaxyAutoMapper(builder);
            if (configurations != default)
                InitializeAutoMappings(configurations);
            return builder;
        }
        private static void InitializeAutoMappings(Action<IMapperConfigurationExpression> configurations)
        {
            Mapper.Initialize(configurations);
        }
    }
    

}
