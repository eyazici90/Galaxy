using Galaxy.ObjectMapping;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galaxy.Mapster
{
   public class MapsterObjectMapper : IObjectMapper
    {
        public TDestination MapTo<TSource, TDestination>(TSource source, TDestination destination)
        {
            return source.Adapt(destination);
        }

        public TDestination MapTo<TDestination>(object source)
        {
            return source.Adapt<TDestination>();
        }

        public IQueryable<TDestination> MapTo<TDestination>(IQueryable source)
        {
            return source.ProjectToType<TDestination>();
        }
    }
}
