using Autofac;
using Galaxy.ObjectMapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Mapster.Bootstrapper.AutoFacModules
{
    public class MapsterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MapsterObjectMapper>()
                .As<IObjectMapper>()
                .SingleInstance();
        }
    }
}
