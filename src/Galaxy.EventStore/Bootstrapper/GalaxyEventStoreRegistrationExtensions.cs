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

        public static ContainerBuilder UseGalaxyEventStore(this ContainerBuilder builder, Action<IGalaxyEventStoreConfigurations> configurationsAction)
        {
            var b =  RegisterGalaxyEventStoreCoreModules(builder, configurationsAction);
            return  ConnectToEventStore(b, configurationsAction).ConfigureAwait(false)
                .GetAwaiter().GetResult();
        }

        private static ContainerBuilder RegisterGalaxyEventStoreCoreModules(this ContainerBuilder builder, Action<IGalaxyEventStoreConfigurations> configurationsAction)
        {
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new UnitOfWorkModule());
            builder.RegisterModule(new NewtonSoftSerializerModule());

            builder.Register(c => {
                var configs = new GalaxyEventStoreConfigurations();
                configurationsAction(configs);
                return configs;
            })
            .As<IGalaxyEventStoreConfigurations>()
            .SingleInstance();

            return builder;
        }

        private static async Task<ContainerBuilder> ConnectToEventStore(this ContainerBuilder builder, Action<IGalaxyEventStoreConfigurations> configurationsAction)
        {
            var configs = new GalaxyEventStoreConfigurations();
            configurationsAction(configs);

            ConnectionSettings settings = ConnectionSettings.Create()
                                                            .SetDefaultUserCredentials(new UserCredentials(configs.username, configs.password)).Build();

            builder.Register(c => {
                IEventStoreConnection connection = EventStoreConnection.Create(settings, new Uri(configs.uri));
                 connection.ConnectAsync().ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
                return connection;
            })
            .As<IEventStoreConnection>()
            .SingleInstance();
            return builder;
        }
    }
}
