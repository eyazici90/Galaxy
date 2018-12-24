using Autofac;
using Galaxy.Log;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.NLog.Bootstrapper.AutoFacModules
{
   
    public class NLogDefaultConfigurationsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GalaxyNLogDefaultConfigurations>()
                       .As<ILogConfigurations>()
                       .SingleInstance();
        }
    }
}
