using Autofac;
using Galaxy.DataContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Dapper.Extensions
{
    public static class DapperExtensions
    {
        public static void CheckConnectionProviderAndNullRegisterForLastChance(this ContainerBuilder builder)
        {
            builder.RegisterType<NullDbConnectionProvider>()
              .As<IActiveDbConnectionProvider>()
              .IfNotRegistered(typeof(IActiveDbConnectionProvider)); 
        }
    }
}
