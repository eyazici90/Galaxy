using Autofac;
using Galaxy.Bootstrapping.AutoFacModules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Bootstrapping
{
    public abstract class GalaxyBaseBootstrapper : IBootstrapContainer<IContainer>
    {
        public virtual IContainer RegisterGalaxyContainer() => this.SharedBuilder().Build();

        public virtual ContainerBuilder RegisterGalaxyContainerBuilder() => this.SharedBuilder();

        private  ContainerBuilder SharedBuilder()
        {
            var container = new ContainerBuilder();
            container.RegisterAssemblyModules(typeof(MediatrModule).Assembly);
            return container;
        }
    }
}
