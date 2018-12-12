using Autofac;
using Galaxy.Redis.Bootstrapper.AutoFacModules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Redis
{
    public static class GalaxyRedisRegistrationExtensions
    {
        public static ContainerBuilder UseGalaxyRedisCache(this ContainerBuilder builder)
        { 
            builder.RegisterModule(new RedisGlobalSettingsModule());
            builder.RegisterModule(new NewtonSoftSerializerModule()); 

            //Redis

            return builder;
        } 
    }
}
