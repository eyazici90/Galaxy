using Autofac;
using Galaxy.Bootstrapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.TestBase
{
    public abstract class LocalTestBaseResolver : IDisposable
    {
        protected ContainerBuilder ContainerBuilder;
        protected IResolver Resolver;
        protected LocalTestBaseResolver()
        {
            ContainerBuilder = GalaxyCoreModule.New
                .RegisterGalaxyContainerBuilder();
        }

        protected LocalTestBaseResolver Build(Action<ContainerBuilder> buildAction)
        {
            buildAction(ContainerBuilder);
            return this;
        }

        public void Initialize()
        {
            var container =  this.ContainerBuilder
                .InitializeGalaxy();

            this.Resolver = container.Resolve<IResolver>();
        }

        protected T TheObject<T>()
        {
            return this.Resolver.Resolve<T>();
        }

        public void Dispose()
        {
            this.ContainerBuilder = null;
            this.Resolver = null;
        }
    }
}
