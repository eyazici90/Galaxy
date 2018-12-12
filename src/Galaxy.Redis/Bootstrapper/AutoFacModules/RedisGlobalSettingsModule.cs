using Autofac;
using Galaxy.Cache;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Redis.Bootstrapper.AutoFacModules
{ 
    public class RedisGlobalSettingsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RedisGlobalDefaultCacheSettings>()
                .As<ICacheDefaultSettings>()
                .SingleInstance();
        }
    }
}
