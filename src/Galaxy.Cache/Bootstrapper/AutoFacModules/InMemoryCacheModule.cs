using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Cache.Bootstrapper.AutoFacModules
{
    public class InMemoryCacheModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InMemoryCache>()
                .As<ICache>()
                .SingleInstance();
        }
    }
}
