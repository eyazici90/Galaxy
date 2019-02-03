using Autofac;

namespace Galaxy.Dapper.Bootstrapper.AutoFacModules
{
    public class DapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DapperRepository>()
                .As<IDapperRepository>()
                .InstancePerDependency();

            builder.RegisterGeneric(typeof(DapperRepository<>))
                .As(typeof(IDapperRepository<>))
                .InstancePerDependency();

            builder.RegisterGeneric(typeof(DapperRepository<,>))
                .As(typeof(IDapperRepository<,>))
                .InstancePerDependency();
            

        }
    }
}
