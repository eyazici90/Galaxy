using Autofac;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using Galaxy.EventStore.Bootstrapper.AutoFacModules;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.EventStore.Bootstrapper
{
   public static  class GalaxyEventStoreRegistrationExtensions
    {

        public static ContainerBuilder UseGalaxyEventStore(this ContainerBuilder builder, Func<IGalaxyEventStoreConfigurations> configurationsAction)
        {
          var b =  RegisterGalaxyEventStoreCoreModules(builder);
            ConnectToEventStore(configurationsAction).ConfigureAwait(false)
                .GetAwaiter().GetResult();
            return b;
        }

        private static ContainerBuilder RegisterGalaxyEventStoreCoreModules(this ContainerBuilder builder)
        {
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new UnitOfWorkModule());
            builder.RegisterModule(new NewtonSoftSerializerModule());
            return builder;
        }

        private static async Task ConnectToEventStore(Func<IGalaxyEventStoreConfigurations> configurationsAction)
        {
            var configs = configurationsAction();

            ConnectionSettings settings = ConnectionSettings.Create()
                                                            .SetDefaultUserCredentials(new UserCredentials(configs.username, configs.password)).Build();

            IEventStoreConnection connection = EventStoreConnection.Create(settings, new Uri(configs.uri));
            await connection.ConnectAsync();
        }
    }
}
