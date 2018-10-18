using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Tests.Domain.AggregatesModel.CustomerAggregate
{
    public class CustomerCreatedDomainEvent : INotification
    {
        private readonly Customer _customer;
        public CustomerCreatedDomainEvent( Customer customer)
        {
            this._customer = customer;
        }
    }
}
