using Autofac;
using Galaxy.Bootstrapping.AutoFacModules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Bootstrapping
{
    public  class GalaxyMainBootsrapper : GalaxyBaseBootstrapper, IBootsrapper
    {
        private static readonly object objlock = new object();

        public static IContainer Container { get; set; }

        public static  ContainerBuilder SingleInstanceBuilder;


        private GalaxyMainBootsrapper()
        {
            SingleInstanceBuilder = SingleInstanceBuilder ?? base.RegisterContainerBuilder();
        }

        public static GalaxyMainBootsrapper Create()
        {
            lock (objlock)
            {
                return new GalaxyMainBootsrapper();
            }
        }

    
       
    }
}
