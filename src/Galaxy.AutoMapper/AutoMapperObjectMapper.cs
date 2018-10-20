using AutoMapper;
using AutoMapper.QueryableExtensions;
using Galaxy.ObjectMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IObjectMapper = Galaxy.ObjectMapping.IObjectMapper;

namespace Galaxy.AutoMapper
{
    public sealed class AutoMapperObjectMapper : IObjectMapper
    {
        public TDestination MapTo<TSource, TDestination>(TSource source, TDestination destination)
        {
            return Mapper.Map<TDestination>(source);
        }

        public TDestination MapTo<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }

        public IQueryable<TDestination> MapTo<TDestination>(IQueryable source)
        {
            return source.ProjectTo<TDestination>();
        }

        
    }
}
