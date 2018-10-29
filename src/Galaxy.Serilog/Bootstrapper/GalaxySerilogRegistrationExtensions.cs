using Autofac;
using Galaxy.Serilog.Bootstrapper.AutoFacModules;
using Serilog;
using System;

namespace Galaxy.Serilog.Bootstrapper
{
    public  static class GalaxySerilogRegistrationExtensions
    {

        public static ContainerBuilder UseGalaxySerilogger(this ContainerBuilder builder, Action<LoggerConfiguration> configurations)
        {
            builder.RegisterModule(new SerilogModule());
            builder.RegisterModule(new SerilogDefaultConfigurationsModule());
            configurations(new LoggerConfiguration());
            return builder;
        }


        public static ContainerBuilder UseGalaxySerilogger<TConfiguration>(this ContainerBuilder builder, Action<LoggerConfiguration> configurations, TConfiguration Tconfigurations )
            where TConfiguration: Log.ILogConfigurations
        {
            builder.RegisterModule(new SerilogModule());
            configurations(new LoggerConfiguration());
            InitializeConfigurations(builder, Tconfigurations);
            return builder;
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
