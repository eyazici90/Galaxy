using Autofac;
using Galaxy.Log;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Serilog.Bootstrapper.AutoFacModules
{
  
    public class SerilogModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GalaxySerilogLogger>()
                 .As<ILog>()
                 .SingleInstance();

        }
    }
}
