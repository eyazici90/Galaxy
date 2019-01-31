using Autofac;
using Galaxy.Dapper.Bootstrapper.AutoFacModules;
using System;

namespace Galaxy.Dapper.Bootstrapper
{
    public static class GalaxyDapperRegistrationExtensions
    {
        public static ContainerBuilder UseGalaxyDapper(this ContainerBuilder builder, Action<DapperGlobalSettings> configureGlobalSettings) 
        {
            var settings = new DapperGlobalSettings();
            configureGlobalSettings(settings);

            builder.RegisterInstance(settings).AsSelf().SingleInstance();
            
            builder.RegisterAssemblyModules(typeof(DapperModule).Assembly);

            return builder;
        }
    }
}
