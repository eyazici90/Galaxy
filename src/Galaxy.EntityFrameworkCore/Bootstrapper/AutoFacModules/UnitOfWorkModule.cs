using Autofac;
using Galaxy.UnitOfWork;

namespace Galaxy.EntityFrameworkCore.Bootstrapper.AutoFacModules
{
    public class UnitOfWorkModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<Galaxy.EFCore.EFUnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<Galaxy.EFCore.EFUnitOfWork>()
               .As<IUnitOfWorkAsync>()
               .InstancePerLifetimeScope();
        }
    }
}
