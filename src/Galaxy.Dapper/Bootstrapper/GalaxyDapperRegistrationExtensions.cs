using Autofac;
using Galaxy.Dapper.Bootstrapper.AutoFacModules;
using Galaxy.Dapper.Extensions;
using Galaxy.DataContext;
using Galaxy.Exceptions;
using System;

namespace Galaxy.Dapper.Bootstrapper
{
    public static class GalaxyDapperRegistrationExtensions
    {
        public static ContainerBuilder UseGalaxyDapper(this ContainerBuilder builder, Action<IDapperSettings> configureGlobalSettings) 
        {
            EnsureDapperPropertiesKeysRegistred(builder);

            builder.RegisterInstance(GetConfiguredDapperSettings(configureGlobalSettings))
               .As<IDapperSettings>()
                 .SingleInstance();
            
            builder.RegisterAssemblyModules(typeof(DapperModule).Assembly);

            return builder;
        }

        public static ContainerBuilder UseGalaxyDapper<TDBConnectionProvider>(this ContainerBuilder builder, TDBConnectionProvider dbConnectionProvider
            , Action<IDapperSettings> configureGlobalSettings) where TDBConnectionProvider : IActiveDbConnectionProvider
        {
            builder.RegisterType<TDBConnectionProvider>()
                .As<IActiveDbConnectionProvider>()
                .InstancePerDependency();

            builder.RegisterInstance(GetConfiguredDapperSettings(configureGlobalSettings))
               .As<IDapperSettings>()
                 .SingleInstance();

            builder.RegisterAssemblyModules(typeof(DapperModule).Assembly);

            builder.CheckConnectionProviderAndNullRegisterForLastChance();

            return builder;
        }

        private static IDapperSettings GetConfiguredDapperSettings(Action<IDapperSettings> configureGlobalSettings)
        {
            var settings = new DapperGlobalSettings();
            configureGlobalSettings(settings);
            return settings;
        }

        private static void EnsureDapperPropertiesKeysRegistred(ContainerBuilder builder)
        {
            if (!builder.Properties.ContainsKey(GalaxyConsts.DbConnectionRegistrationKey))
            {
                throw new GalaxyException($"Dapper registration should be after EntityFrameworkCore registration" +
                                                       $" use UseGalaxyEntityFrameworkCore() methods before {nameof(UseGalaxyDapper)}.");
            }
        }

    }
}
