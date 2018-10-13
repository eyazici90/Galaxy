
using Autofac;
using MediatR;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Module = Autofac.Module;

namespace Galaxy.Bootstrapping.AutoFacModules
{
    public class MediatrModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsClosedTypesOf(typeof(INotificationHandler<>));


            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

        }
    }
}


