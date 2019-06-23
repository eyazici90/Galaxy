﻿using Galaxy.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public static string GetIdentifierFromStreamId(string identifier) =>
           string.Join("-"
               , identifier.Split("-").Skip(1).Take(100));

    }
}
