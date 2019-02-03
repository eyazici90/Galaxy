using Autofac;
using Galaxy.EFCore;
using Galaxy.UnitOfWork;

namespace Galaxy.EntityFrameworkCore.Bootstrapper.AutoFacModules
{
    public class UnitOfWorkModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<EFUnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EFUnitOfWork>()
               .As<IUnitOfWorkAsync>()
               .InstancePerLifetimeScope();
        }
    }
}
