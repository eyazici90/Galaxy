using Autofac;
using Galaxy.Bootstrapping.AutoFacModules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Bootstrapping
{
    public  class GalaxyMainBootsrapper : GalaxyBaseBootstrapper
    {
        public  static IContainer Container { get; set; }

        public static  ContainerBuilder SingleInstanceBuilder;

        public GalaxyMainBootsrapper()
        {
            SingleInstanceBuilder = SingleInstanceBuilder ?? base.RegisterContainerBuilder();
        }

    }
}
