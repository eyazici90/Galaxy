using Autofac;
using Galaxy.Resilence.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Resilence.Bootstrapper.AutoFacModules
{
    public class HttpModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StandardHttpClient>()
                .As<IHttpClient>()
                .InstancePerDependency();
        }
    }
}
