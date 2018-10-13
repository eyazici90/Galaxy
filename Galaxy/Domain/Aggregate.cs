using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Domain
{
    public class Aggregate
    {
        public string Identifier { get; private set; }

        public int Version { get; private set; }

        public string RootType { get; private set; }

        public IAggregateRoot Root { get; private set; }

        public Aggregate(string identifier, int expectedVersion, IAggregateRoot root)
        {
            this.Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
            this.Version = expectedVersion;
            this.Root = root ?? throw new ArgumentNullException(nameof(root));
            this.RootType = root.GetType().Name;
        }
    }
}
