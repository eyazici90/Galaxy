using Galaxy.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Domain.AggregatesModel.TenantAggregate
{
    public sealed class Tenant : AggregateRootEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public string Logo { get; private set; }

        private Tenant()
        {
        }

        private Tenant(string name, string desc = default) : this()
        {
            this.Name = !string.IsNullOrWhiteSpace(name) ? name
                                                         : throw new ArgumentNullException(nameof(name));

            this.Description = desc;
        }

        public static Tenant Create(string name, string desc = default)
        {
            return new Tenant(name, desc);
        }

        public Tenant ChangeName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
                this.Name = name;

            return this;
        }

        public Tenant ChangeOrAddDesc(string desc)
        {
            if (!string.IsNullOrWhiteSpace(desc))
                this.Description = desc;

            return this;
        }
    }
}
