using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.EventStore
{
    public class EventMetadata
    {
        public DateTime TimeStamp { get; set; }

        public string AggregateType { get; set; }

        public string AggregateAssemblyQualifiedName { get; set; }

        public bool IsSnapshot { get; set; }

        public int Version { get; set; }
    }
}
