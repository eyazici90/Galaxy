using Autofac;
using EventStoreSample.Listener.Consumers;
using Galaxy.Bootstrapping;
using Galaxy.RabbitMQ.Bootstrapper;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EventStoreSample.Listener
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"{DateTime.Now} : Listener Started !!!");


            var containerBuilder = GalaxyCoreModule.New
                 .RegisterGalaxyContainerBuilder()
                     .UseGalaxyCore()
                     .UseGalaxyRabbitMQ(conf => {
                         conf.Username = "guest";
                         conf.Password = "guest";
                         conf.HostAddress = "rabbitmq://localhost/";
                         conf.QueueName = "eventStoreSampleSub";
                     }, typeof(TransactionCreatedConsumer).Assembly);

          
                     
            var container = containerBuilder.InitializeGalaxy();

            var busControl = container.Resolve<IBusControl>();
            busControl.StartAsync()
                .ConfigureAwait(false)
                .GetAwaiter().GetResult();

            Console.ReadLine();
        }
    }
}
