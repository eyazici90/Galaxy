using CustomerSample.Customer.Domain.AggregatesModel.BrandAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerSample.Domain.Events
{
    public class BrandCreatedDomainEvent : INotification
    {
        public Brand Brand { get; private set; }

        public BrandCreatedDomainEvent(Brand brand)
        {
            this.Brand = brand;
        }
    }
}
