using Autofac;
using Galaxy.NLog.Bootstrapper.AutoFacModules;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.NLog.Bootstrapper
{ 
    public static class GalaxyNLogRegistrationExtensions
    {

        public static ContainerBuilder UseGalaxyNLogger(this ContainerBuilder builder)
        {
            builder.RegisterModule(new NLogModule());
            builder.RegisterModule(new NLogDefaultConfigurationsModule());
            return builder;
        }

        public static ContainerBuilder UseGalaxyNLogger<TConfiguration>(this ContainerBuilder builder, TConfiguration Tconfigurations)
            where TConfiguration : Log.ILogConfigurations
        {
            builder.RegisterModule(new NLogModule());
            InitializeNLoggerConfiguration(builder);
            InitializeConfigurations(builder, Tconfigurations);
            return builder;
        }

        private static void InitializeNLoggerConfiguration(this ContainerBuilder builder)
        { 
            builder.Register(c =>
            {
                return LogManager.GetCurrentClassLogger();
            })
          .As<global::NLog.ILogger>()
          .SingleInstance();
        }

        private static void InitializeConfigurations<TConfiguration>(this ContainerBuilder builder, TConfiguration configurations = default)
            where TConfiguration : Log.ILogConfigurations
        {
            builder.RegisterType(typeof(TConfiguration))
                    .As<Log.ILogConfigurations>()
                    .SingleInstance();
        }
    }
}
