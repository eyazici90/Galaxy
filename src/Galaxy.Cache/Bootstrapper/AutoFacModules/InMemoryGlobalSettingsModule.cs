using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Cache.Bootstrapper.AutoFacModules
{
  
    public class InMemoryGlobalSettingsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InMemoryGlobalDefaultCacheSettings>()
                .As<ICacheDefaultSettings>()
                .SingleInstance();
        }
    }
}
