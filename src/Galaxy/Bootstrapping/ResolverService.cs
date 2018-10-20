using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Bootstrapping
{
    public class ResolverService : IResolver
    {
        public T Resolve<T>()
        {
            using (var scope = GalaxyCoreModule.Container.BeginLifetimeScope())
            {
                return scope.Resolve<T>();
            }
        }

        public object Resolve(object obj)
        {
            using (var scope = GalaxyCoreModule.Container.BeginLifetimeScope())
            {
                return scope.Resolve(obj.GetType());
            }
        }

        public object Resolve(Type type)
        {
            using (var scope = GalaxyCoreModule.Container.BeginLifetimeScope())
            {
                return scope.Resolve(type);
            }
        }


        public object ResolveOptional(Type type)
        {
            using (var scope = GalaxyCoreModule.Container.BeginLifetimeScope())
            {
                return scope.ResolveOptional(type);
            }
        }

    }
}
