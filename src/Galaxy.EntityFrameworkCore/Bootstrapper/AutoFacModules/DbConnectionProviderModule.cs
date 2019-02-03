using Autofac;
using Galaxy.DataContext;
using Galaxy.EFCore;
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

            builder.RegisterType<ActiveDbConnectionProvider>()
               .As<IActiveDbTransactionProvider>().InstancePerDependency();
        }
    }
}
