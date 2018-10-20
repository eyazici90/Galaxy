using Autofac;
using Galaxy.Resilence.Bootstrapper.AutoFacModules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Resilence.Bootstrapper
{
    public static class GalaxyResilenceHttpRegistrationExtensions
    {
        public static ContainerBuilder UseGalaxyStandardHttpClient(this ContainerBuilder builder)
        {
            builder.RegisterModule(new HttpModule());
            return builder;
        }
        public static ContainerBuilder UseGalaxyStandardHttpClient(this ContainerBuilder builder, Action<ContainerBuilder> action)
        {
            UseGalaxyStandardHttpClient(builder);
            action(builder);
            return builder;
        }

    }
    
}
