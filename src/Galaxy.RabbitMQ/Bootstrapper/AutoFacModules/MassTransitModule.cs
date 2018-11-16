using Autofac;
using GreenPipes;
using MassTransit;
using MassTransit.RabbitMqTransport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.RabbitMQ.Bootstrapper.AutoFacModules
{
    public class MassTransitModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctx =>
            {
                var configuration = ctx.Resolve<IGalaxyRabbitMQConfiguration>();

                IBusControl busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    IRabbitMqHost host = cfg.Host(new Uri(configuration.HostAddress), h =>
                    {
                        h.Username(configuration.Username);
                        h.Password(configuration.Password);
                    });

                    cfg.ReceiveEndpoint(host, configuration.QueueName, ec => { ec.LoadFrom(ctx); });

                    cfg.AutoDelete = configuration.AutoDeleted;  

                    if (configuration.UseRetryMechanism) cfg.UseRetry(rtryConf => { rtryConf.Immediate(configuration.MaxRetryCount); });

                    if (configuration.PrefetchCount.HasValue) cfg.PrefetchCount = (ushort)configuration.PrefetchCount;

                    if (configuration.ConcurrencyLimit.HasValue) cfg.UseConcurrencyLimit(configuration.ConcurrencyLimit.Value);
                    
                });

                return busControl;

            }).SingleInstance()
                .As<IBusControl>()
                .As<IBus>();

        }
    }
}
