using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.ElasticSearch.Bootstrapper
{
    public static  class GalaxyElasticSearchRegistrationExtensions
    {
        public static ContainerBuilder UseGalaxyElasticSearch(this ContainerBuilder builder)
        { 
            return builder;
        }
    }
}
