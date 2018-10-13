using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Galaxy.ObjectMapping
{
    public interface IObjectMapper
    {
            TDestination MapTo<TDestination>(object source);
            
            TDestination MapTo<TSource, TDestination>(TSource source, TDestination destination);

            IQueryable<TDestination> MapTo<TDestination>(IQueryable source);
    }
}
