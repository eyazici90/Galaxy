using Mapster;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Galaxy.Mapster
{
    internal static class MapsterConfigurationExtensions
    {
        public static void CreateAutoAttributeMaps( this TypeAdapterConfig configuration, Type type)
        {
            foreach (AutoMapAttributeBase autoMapAttribute in type.GetTypeInfo().GetCustomAttributes<AutoMapAttributeBase>())
            {
                autoMapAttribute.CreateMap(configuration, type);
            }
        }
    }
}
