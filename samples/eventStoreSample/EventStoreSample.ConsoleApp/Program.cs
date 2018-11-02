using Autofac;
using EventStore.ClientAPI;
using EventStoreSample.ConsoleApp.DomainModel;
using Galaxy.Bootstrapping;
using Galaxy.EventStore;
using Galaxy.EventStore.Bootstrapper;
using Galaxy.Repositories;
using Galaxy.UnitOfWork;
using System;

namespace EventStoreSample.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("EventStore App Started!");

            var galaxyContainer = GalaxyCoreModule.New
                .RegisterContainerBuilder()
                .UseGalaxyCore()
                .UseGalaxyEventStore(() =>
                {
                    return new GalaxyEventStoreConfigurations
                    {
                        username = "admin",
                        password = "changeit",
                        uri = "tcp://admin:changeit@localhost:1113"
                    };
                })
                .InitializeGalaxy();

            using (var scope = GalaxyCoreModule.Container.BeginLifetimeScope())
            {
                var resolver = scope.Resolve<IResolver>();
                var userRepository = scope.Resolve<IRepositoryAsync<User>>();
                var unitOfworkAsync = scope.Resolve<IUnitOfWorkAsync>();

                for (int i = 0; i < 100 ; i++)
                {
                    var newUser = User.Create($"Emre - {i}", $"Yazıcı - {i}" );
                    userRepository.Insert(newUser);
                    unitOfworkAsync.SaveChangesAsync().ConfigureAwait(false)
                        .GetAwaiter().GetResult();
                }
            }
            Console.ReadLine();
        }


    }

 
}
