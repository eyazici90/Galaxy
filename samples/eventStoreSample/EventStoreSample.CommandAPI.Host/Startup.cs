using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using EventStoreSample.Application.Commands.Handlers;
using EventStoreSample.Application.DomainEventHandlers;
using EventStoreSample.Application.Validations;
using Galaxy.Bootstrapping;
using Galaxy.Commands;
using Galaxy.EventStore.Bootstrapper;
using Galaxy.FluentValidation;
using Galaxy.FluentValidation.Bootstrapper;
using Galaxy.Mapster.Bootstrapper;
using Galaxy.RabbitMQ.Bootstrapper;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace EventStoreSample.CommandAPI.Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Payment API", Version = "v1" });
            });
            
            services.AddOptions();

            services.AddMvc()
             .AddJsonOptions(options =>
             {
                 options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                 options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
             })
            .AddControllersAsServices();

            var container = this.ConfigureGalaxy(services);

            var busControl = container.Resolve<IBusControl>();
            busControl.StartAsync()
                .ConfigureAwait(false)
                .GetAwaiter().GetResult();

            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });

            app.UseSwagger()
             .UseSwaggerUI(c =>
             {
                 c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment API V1");
             });

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{action}/{id?}");
            });
        }

        private IContainer ConfigureGalaxy(IServiceCollection services)
        {
            var containerBuilder = GalaxyCoreModule.New
                 .RegisterGalaxyContainerBuilder()
                     .UseGalaxyCore(b=> {
                         b.UseConventionalDomainEventHandlers(typeof(TransactionCreatedDomainEventHandler).Assembly);
                         b.UseConventionalCommandHandlers(typeof(DirectPaymentCommandHandler).Assembly);

                         b.RegisterAssemblyTypes(typeof(DirectPaymentCommandHandler).Assembly)
                            .AssignableTo<ICommandHandler>()
                            .AsImplementedInterfaces()
                            .EnableInterfaceInterceptors()
                            .InterceptedBy(typeof(ValidatorInterceptor))
                            .InstancePerLifetimeScope();
                     })
                     .UseGalaxyEventStore((configs) =>
                      {

                          configs.username = "admin";
                          configs.password = "changeit";
                          configs.uri = "tcp://admin:changeit@localhost:1113";

                      })
                     .UseGalaxyRabbitMQ(conf => {
                           conf.Username = "guest";
                           conf.Password = "guest";
                           conf.HostAddress = "rabbitmq://localhost/";
                           conf.QueueName = "eventStoreSample";
                       })
                     .UseGalaxyMapster()
                     .UseGalaxyFluentValidation(typeof(DirectPaymentCommandValidation).Assembly);

            containerBuilder.Populate(services);

            return containerBuilder.InitializeGalaxy();

        }
    }
}
