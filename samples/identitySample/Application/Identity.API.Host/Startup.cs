using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Galaxy.Application;
using Galaxy.Bootstrapping;
using Galaxy.EntityFrameworkCore.Bootstrapper;
using Galaxy.Identity.Bootstrapper;
using Galaxy.Mapster.Bootstrapper;
using Galaxy.NewtonSoftJson.Bootstrapper;
using Galaxy.UnitOfWork;
using Identity.Application.Services;
using Identity.Domain.AggregatesModel.RoleAggregate;
using Identity.Domain.AggregatesModel.UserAggregate;
using Identity.EFRepositories.UserAggregate;
using Identity.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace Identity.API.Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
         
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IdentityContext>(options =>
                  options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Identity API", Version = "v1" });
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddOptions();

            services.AddMvc()
             .AddJsonOptions(options =>
             {
                 options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                 options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
             })
            .AddControllersAsServices();

            var container = this.ConfigureGalaxy(services);

            return new AutofacServiceProvider(container);
            
        }
         
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
                 c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity API V1");
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
                     .UseGalaxyCore(b =>
                     {
                         b.UseConventionalCustomRepositories(typeof(UserRepository).Assembly);  
                         b.UseConventionalApplicationService(typeof(UserAppService).Assembly);  

                         b.RegisterAssemblyTypes(typeof(UserAppService).Assembly)
                              .AssignableTo<IApplicationService>()
                              .AsImplementedInterfaces()
                              .EnableInterfaceInterceptors()
                              .InterceptedBy(typeof(UnitOfWorkInterceptor))
                              .InstancePerLifetimeScope();
                     })
                     .UseGalaxyEntityFrameworkCore<IdentityContext>(conf =>
                     {
                         conf.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                     })
                     .UseGalaxyNewtonSoftJsonSerialization()
                     .UseGalaxyMapster()
                     .UseGalaxyIdentity<IdentityContext, User, Role, int>(services, opt =>
                     {
                         opt.Lockout = new LockoutOptions()
                         {
                             AllowedForNewUsers = true,
                             DefaultLockoutTimeSpan = TimeSpan.FromDays(30),
                             MaxFailedAccessAttempts = 3
                         };
                         opt.Password.RequireDigit = false;
                         opt.Password.RequiredLength = 6;
                         opt.Password.RequireNonAlphanumeric = false;
                         opt.Password.RequireUppercase = false;
                         opt.Password.RequireLowercase = false;
                     });

            containerBuilder.Populate(services);

            return containerBuilder.InitializeGalaxy();

        }
    }
}
