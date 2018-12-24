using Autofac;
using Galaxy.Bootstrapping.AutoFacModules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Bootstrapping
{
    public  class GalaxyCoreModule : GalaxyBaseBootstrapper, IBootsrapper
    {
        private static readonly object objlock = new object();

        public static IContainer Container { get; set; }

        public static  ContainerBuilder SingleInstanceBuilder;


        private GalaxyCoreModule()
        {
            SingleInstanceBuilder = SingleInstanceBuilder ?? base.RegisterGalaxyContainerBuilder();
        }

        private static GalaxyCoreModule Create()
        {
            lock (objlock)
            {
                return new GalaxyCoreModule();
            }
        }

        public static GalaxyCoreModule New => Create();
        
    }
}
