using Galaxy.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Tests.Domain.AggregatesModel.CustomerAggregate
{
    public class Brand : Entity<long>
    {
        public string Name { get; set; }
    }
}
