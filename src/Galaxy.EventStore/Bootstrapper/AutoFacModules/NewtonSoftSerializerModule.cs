using Autofac;
using Galaxy.Serialization;

namespace Galaxy.EventStore.Bootstrapper.AutoFacModules
{
    public class NewtonSoftSerializerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NewtonsoftJsonSerializer>()
                .As<ISerializer>()
                .SingleInstance();
        }
    }
}
