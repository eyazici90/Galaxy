using Autofac;
using Galaxy.Log;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.NLog.Bootstrapper.AutoFacModules
{ 
    public class NLogModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GalaxyNLogLogger>()
                 .As<ILog>()
                 .SingleInstance();

        }
    }
}
