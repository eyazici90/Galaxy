using Autofac;
using Galaxy.EFCore;
using Galaxy.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.EntityFrameworkCore.Bootstrapper.AutoFacModules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Repository<>))
               .As(typeof(IRepository<>))
               .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Repository<>))
               .As(typeof(IRepository<,>))
               .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Repository<>))
               .As(typeof(IRepositoryAsync<>))
               .InstancePerLifetimeScope();
        }
    }
}
