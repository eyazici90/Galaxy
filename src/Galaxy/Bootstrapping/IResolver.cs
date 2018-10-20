using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Bootstrapping
{
   public interface IResolver
    {
        T Resolve<T>();
        object Resolve(object obj);
        object Resolve(Type type);
        object ResolveOptional(Type type);
    }
}
