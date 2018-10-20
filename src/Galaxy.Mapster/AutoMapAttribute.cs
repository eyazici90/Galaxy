using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Mapster
{
    public class AutoMapAttribute : AutoMapAttributeBase
    {
        public AutoMapAttribute(params Type[] targetTypes)
            : base(targetTypes)
        {
        }

        public override void CreateMap(TypeAdapterConfig configuration, Type needstoMap)
        {
            if (TargetTypes == null || TargetTypes.Length == 0)
                return;
            
            foreach (Type source in TargetTypes)
            {
                configuration.NewConfig(source, needstoMap);
                configuration.NewConfig(needstoMap, source);
            }
        }
    }
}
