
using Galaxy.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerSample.Customer.Domain.AggregatesModel.GroupAggregate
{
    public class Group : Entity<Guid> , IAggregateRoot
    {
        public string Name { get; private set; }
    }
}
