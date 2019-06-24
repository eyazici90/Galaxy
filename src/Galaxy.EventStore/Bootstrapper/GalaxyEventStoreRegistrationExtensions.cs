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
            var configs = new GalaxyEventStoreConfigurations();

            configurationsAction(configs);

            if (configs.IsSnapshottingOn) 
            {
                builder.RegisterModule(new SnapshotableRepositoryModule());
            }
            else
            {
                builder.RegisterModule(new RepositoryModule());
            }
            builder.RegisterModule(new SnapshotReaderModule()); 
            builder.RegisterModule(new UnitOfWorkModule());

            builder.Register(c => configs)
            .As<IGalaxyEventStoreConfigurations>()
            .SingleInstance();

            return builder;
        }

        private static async Task<ContainerBuilder> ConnectToEventStore(this ContainerBuilder builder, Action<IGalaxyEventStoreConfigurations> configurationsAction)
        {
            var configs = new GalaxyEventStoreConfigurations();
            configurationsAction(configs);

            ConnectionSettings settings = ConnectionSettings.Create()
                                                            .SetDefaultUserCredentials(new UserCredentials(configs.Username, configs.Password)).Build();

            builder.Register(c => {
                IEventStoreConnection connection = EventStoreConnection.Create(settings, new Uri(configs.Uri));
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
