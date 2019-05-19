using Autofac;
using Galaxy.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Bootstrapping.AutoFacModules
{
    
    public class InterceptorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWorkInterceptor>()
              .AsSelf();
        }
    }
}
