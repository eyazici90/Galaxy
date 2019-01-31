using Autofac;
using Galaxy.DataContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.EntityFrameworkCore.Bootstrapper.AutoFacModules
{
    public class DbConnectionProviderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ActiveDbConnectionProvider>()
                .As<IActiveDbConnectionProvider>().InstancePerDependency();
        }
    }
}
