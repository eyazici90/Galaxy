using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Galaxy.Bootstrapping;
using Galaxy.Commands;
using Galaxy.EFCore;
using Galaxy.EntityFrameworkCore.Bootstrapper;
using Galaxy.FluentValidation;
using Galaxy.FluentValidation.Bootstrapper;
using Galaxy.Mapster.Bootstrapper;
using Galaxy.Repositories;
using Galaxy.Serilog.Bootstrapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PaymentSample.Application.Commands.Handlers;
using PaymentSample.Application.DomainEventHandlers;
using PaymentSample.Application.Validations;
using PaymentSample.Domain.AggregatesModel.PaymentAggregate;
using PaymentSample.Infrastructure;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace PaymentSample.CommandAPI.Host
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
            services.AddDbContext<PaymentSampleDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Payment API", Version = "v1" });
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddOptions();

            services.AddMvc(options =>
            {

            })
             .AddJsonOptions(options =>
             {
                 options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                 options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
             })
            .AddControllersAsServices();

            var container = this.ConfigureGalaxy(services);

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
                     .UseGalaxyCore(b => {

                         b.UseConventionalDomainEventHandlers(typeof(TransactionCreatedDomainEventHandler).Assembly);

                         b.RegisterAssemblyTypes(typeof(DirectPaymentCommandHandler).Assembly)
                              .AssignableTo<ICommandHandler>()
                              .AsImplementedInterfaces()
                              .EnableInterfaceInterceptors()
                              .InterceptedBy(typeof(ValidatorInterceptor))
                              .InstancePerLifetimeScope();
                     })
                     .UseGalaxyEntityFrameworkCore(
                                new DbContextOptionsBuilder<PaymentSampleDbContext>()
                                     .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")))
                     .UseGalaxyMapster()
                     .UseGalaxyFluentValidation(typeof(PaymentTransactionValidation).Assembly)
                     .UseGalaxySerilogger(configs => {
                           configs.WriteTo.File("log.txt",
                              rollingInterval: RollingInterval.Day,
                              rollOnFileSizeLimit: true);
                       });

            containerBuilder.Populate(services);

            return containerBuilder.InitializeGalaxy();

        }
    }
}
