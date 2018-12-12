using Autofac;
using Galaxy.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Redis.Bootstrapper.AutoFacModules
{
    public class NewtonSoftSerializerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NewtonsoftJsonSerializer>()
                .As<ISerializer>()
                .SingleInstance();
        }
    }
}
