using Autofac;
using Galaxy.Cache;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Redis.Bootstrapper.AutoFacModules
{ 
    public class RedisCacheModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RedisCache>()
                .As<ICache>()
                .SingleInstance();
        }
    }
}
