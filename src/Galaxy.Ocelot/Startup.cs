using System;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Galaxy.Bootstrapping;
using Galaxy.Ocelot.Extensions;
using Galaxy.Ocelot.Serialization;
using Galaxy.Serialization;
using Galaxy.Serilog.Bootstrapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;

namespace Galaxy.Ocelot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = new ConfigurationBuilder()
                        .AddJsonFile(Path.Combine("configuration", "configuration.json"), optional: true, reloadOnChange: true)
                        .Build(); 
        }

        public IConfiguration Configuration { get; }
        
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddOcelot(Configuration);

            var container = this.ConfigureGalaxy(services);
            return new AutofacServiceProvider(container);
        }
        
        public  void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            // app.UseCircuitBreakerMiddleware();
            app.UseResponseConsistentMiddleware();
            app.UseIdempotencyMiddleware();
            app.UseLogMiddleware();
            app.UseCorrelationIdMiddleware();

            app.UseOcelot(conf =>
            {
                conf.PreQueryStringBuilderMiddleware = async (ctx, next) =>
                {
                    await next.Invoke();
                };
                conf.PreErrorResponderMiddleware = async (ctx, next) =>
                {
                    await next.Invoke();
                };
            })
            .ConfigureAwait(false)
            .GetAwaiter().GetResult();
        }

        private IContainer ConfigureGalaxy(IServiceCollection services)
        {
            var containerBuilder = GalaxyCoreModule.New
                 .RegisterGalaxyContainerBuilder()
                     .UseGalaxyCore( builder => {
                         builder.RegisterType<NewtonsoftJsonSerializer>()
                           .As<ISerializer>()
                           .SingleInstance();
                     })
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
