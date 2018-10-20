using Autofac;
using Galaxy.UnitOfWork;

namespace Galaxy.EntityFrameworkCore.Bootstrapper.AutoFacModules
{
    public class UnitOfWorkModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<Galaxy.EFCore.UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<Galaxy.EFCore.UnitOfWork>()
               .As<IUnitOfWorkAsync>()
               .InstancePerLifetimeScope();
        }
    }
}
