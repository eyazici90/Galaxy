using Autofac;
using Autofac.Extensions.DependencyInjection;
using Galaxy.Bootstrapping;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.TestBase
{
    public abstract class LocalTestBaseResolver : IDisposable
    {
        protected ContainerBuilder ContainerBuilder;
        protected IContainer Container;
        protected ILifetimeScope Scope;
        protected bool IsContainerInitializedBefore;

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

            var container = this.ContainerBuilder
                .InitializeGalaxy();

            Container = container;

            Scope = Container.BeginLifetimeScope();
            IsContainerInitializedBefore = true;
        }

        public void Initialize(IServiceCollection services)
        {
            this.ContainerBuilder.Populate(services);

            Initialize();
        }

        protected T TheObject<T>()
        {
            return Scope.Resolve<T>();
        }

        public void Dispose()
        {
            //this.Container.Dispose();
            //this.Scope.Dispose();
            //this.ContainerBuilder = null; 
        }
    }
}
