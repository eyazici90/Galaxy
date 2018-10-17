using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Bootstrapping.AutoFacModules
{
    public class BoostrapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GalaxyCoreModule>()
                            .As<IBootsrapper>()
                            .SingleInstance();

            builder.RegisterType<ResolverService>()
                            .As<IResolver>()
                            .SingleInstance();
        }
    }

    
}
