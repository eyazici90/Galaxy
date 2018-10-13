using Autofac;
using Galaxy.ObjectMapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.AutoMapper.Bootstrapper.AutoFacModules
{
    public class AutoMapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AutoMapperObjectMapper>()
                .As<IObjectMapper>()
                .SingleInstance();
        }
    }
}
