using Autofac;
using Galaxy.Serialization; 

namespace Galaxy.Utf8Json.Bootstrapper.AutoFacModules
{ 
    public class Utf8JsonSerializerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Utf8JsonSerializer>()
                .As<ISerializer>()
                .SingleInstance();
        }
    }
}
