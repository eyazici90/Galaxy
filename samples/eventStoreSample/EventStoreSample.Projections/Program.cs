using Autofac;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using EventStoreSample.Domain.AggregatesModel.PaymentAggregate;
using Galaxy.Bootstrapping;
using Galaxy.Domain;
using Galaxy.EventStore;
using Galaxy.EventStore.Bootstrapper;
using Galaxy.Mapster.Bootstrapper;
using Galaxy.NewtonSoftJson;
using Galaxy.NewtonSoftJson.Bootstrapper;
using Galaxy.Repositories;
using Galaxy.Serialization;
using Galaxy.UnitOfWork;
using Galaxy.Utf8Json.Bootstrapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace EventStoreSample.Projections
{
    class Program
    {
        static void Main(string[] args) =>
            MainAsync(args).ConfigureAwait(false)
                    .GetAwaiter().GetResult();
              

        static async Task MainAsync(string[] args)
        {
            Console.WriteLine("Projections Started !!!");

            var container = ConfigureGalaxy();

            await InitProjections(container);

            Console.ReadLine();

        }
        private static IContainer ConfigureGalaxy()
        {
            var containerBuilder = GalaxyCoreModule.New
                 .RegisterGalaxyContainerBuilder()
                     .UseGalaxyCore(b =>
                     { 
                     })
                     .UseGalaxyNewtonSoftJsonSerialization()
                     .UseGalaxyEventStore((configs) =>
                     {

                         configs.Username = "admin";
                         configs.Password = "changeit";
                         configs.Uri = "tcp://admin:changeit@localhost:1113";
                         configs.IsSnapshottingOn = true;
                     }) 
                     .UseGalaxyMapster(); 

            return containerBuilder.InitializeGalaxy();

        }
       

        private static async Task InitProjections(IContainer container)
        { 
            var esConnection = container.Resolve<IEventStoreConnection>(); 
            var defaultSerializer = container.Resolve<ISerializer>();
            var nullCheckpointStore = new NullInstanceCheckpointStore();
            var esUnitOfWork = container.Resolve<IUnitOfWorkAsync>();
            var paymentRootRepo = container.Resolve<IRepositoryAsync<PaymentTransactionState, Guid>>();

          
            await SubscriptionManagerBuilder.New
                .Connection(esConnection)
                .Deserializer(defaultSerializer)
                .CheckpointStore(nullCheckpointStore)
                .Snaphotter(
                    new EventStoreSnapshotter<PaymentTransactionState, Guid, PaymentTransactionSnapshot>(
                        paymentRootRepo,
                        esUnitOfWork,
                        () => esConnection,
                        e => e.Event.EventNumber > 0 && e.Event.EventNumber % 5 == 0,
                        stream => $"{stream}-Snapshot",
                        defaultSerializer
                        ))
                .Projections(
                      new PaymentTransactionProjection()
                ).Activate();
        } 
    }
}
