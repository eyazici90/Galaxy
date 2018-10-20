using Autofac;
using Galaxy.Bootstrapping.AutoFacModules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Bootstrapping
{
    public abstract class GalaxyBaseBootstrapper : IBootstrapContainer<IContainer>
    {
        public virtual IContainer RegisterContainer() => this.SharedBuilder().Build();

        public virtual ContainerBuilder RegisterContainerBuilder() => this.SharedBuilder();

        private  ContainerBuilder SharedBuilder()
        {
            var container = new ContainerBuilder();
            container.RegisterModule(new MediatrModule());
            container.RegisterModule(new InterceptorModule());
            container.RegisterModule(new BoostrapperModule());
            
            return container;
        }
    }
}
