using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Mapster
{
    public abstract class AutoMapAttributeBase : Attribute
    {
        protected AutoMapAttributeBase(params Type[] targetTypes)
        {
            TargetTypes = targetTypes ?? throw new ArgumentNullException(nameof(targetTypes)) ;
        }
        
        public Type[] TargetTypes { get; private set; }

        public abstract void CreateMap(TypeAdapterConfig configuration, Type needstoMap);
    }
}
