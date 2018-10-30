using Autofac;
using Galaxy.Log;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Serilog.Bootstrapper.AutoFacModules
{
   
    public class SerilogDefaultConfigurationsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GalaxySerilogDefaultConfigurations>()
                       .As<ILogConfigurations>()
                       .SingleInstance();
        }
    }
}
