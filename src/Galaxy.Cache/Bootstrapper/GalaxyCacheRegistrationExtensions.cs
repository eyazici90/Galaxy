using Autofac;
using Autofac.Extensions.DependencyInjection;
using Galaxy.Cache.Bootstrapper.AutoFacModules;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Galaxy.Cache.Bootstrapper
{
    public static  class GalaxyCacheRegistrationExtensions
    {
        public static ContainerBuilder UseGalaxyInMemoryCache(this ContainerBuilder builder, IServiceCollection services)
        {
            RegisterInMemoryCacheFromSericeProvider(services);
            builder.RegisterAssemblyModules(typeof(InMemoryCacheModule).Assembly);
            return builder;
        }

        public static ContainerBuilder UseGalaxyInMemoryCache(this ContainerBuilder builder, IServiceCollection services, Action<ICacheDefaultSettings> configureCacheSettings)
        {
            UseGalaxyInMemoryCache(builder, services);

             var settings = new InMemoryGlobalDefaultCacheSettings();

            configureCacheSettings(settings);

            builder.RegisterInstance(settings)
                .As<ICacheDefaultSettings>()
                .SingleInstance();

            return builder;
        }

        private static void RegisterInMemoryCacheFromSericeProvider(IServiceCollection services) =>
            services.AddMemoryCache();
        
    }
}
