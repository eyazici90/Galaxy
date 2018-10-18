using Galaxy.Domain;
using Galaxy.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Tests.Domain.AggregatesModel.CustomerAggregate
{
   public sealed  class Customer : Entity, IAggregateRoot
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public Brand Brand { get; set; }

        private  Customer( string name) : base()
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new GalaxyException($"Invalid name");
            this.Name = name;
            AddDomainEvent(new CustomerCreatedDomainEvent(this));
        }
        public static Customer Create(string name)
        {
            return new Customer(name);
        }

    }
}
