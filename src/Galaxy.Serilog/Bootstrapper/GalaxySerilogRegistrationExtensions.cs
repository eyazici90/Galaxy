using Autofac;
using Galaxy.Log;
using Galaxy.Serilog.Bootstrapper.AutoFacModules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Serilog.Bootstrapper
{
    public  static class GalaxySerilogRegistrationExtensions
    {
        public static ContainerBuilder UseGalaxySerilogger(this ContainerBuilder builder)
        {
            builder.RegisterModule(new SerilogModule());
            builder.RegisterModule(new SerilogDefaultConfigurationsModule());
            return builder;
        }
        public static ContainerBuilder UseGalaxySerilogger<TConfiguration>(this ContainerBuilder builder, TConfiguration configurations )
            where TConfiguration: ILogConfigurations
        {
            builder.RegisterModule(new SerilogModule());
            InitializeConfigurations(builder, configurations);
            return builder;
        }
        private static void InitializeConfigurations<TConfiguration>(this ContainerBuilder builder, TConfiguration configurations = default)
            where TConfiguration : ILogConfigurations
        {
            builder.RegisterType(typeof(TConfiguration))
                    .As<ILogConfigurations>()
                    .SingleInstance();
        }
    }
}
