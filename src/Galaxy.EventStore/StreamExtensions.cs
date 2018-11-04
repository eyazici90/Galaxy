using Galaxy.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.EventStore
{
    public static  class StreamExtensions
    {
        public static string GetStreamName(Aggregate aggregate)
        {
            return $"{aggregate.RootType}-{aggregate.Identifier}";
        }

        public static string GetStreamName(Type aggregateRoot, string identifier)
        {
            return $"{aggregateRoot.Name}-{identifier}";
        }
    }
}
