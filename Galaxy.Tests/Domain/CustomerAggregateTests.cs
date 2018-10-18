using Galaxy.Domain;
using Galaxy.Exceptions;
using Galaxy.Tests.Domain.AggregatesModel.CustomerAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Galaxy.Tests.Domain
{
   public class CustomerAggregateTests
    {
        
        [Fact]
        public void Create_customer_aggregateroot_fail()
        {
            var fakeName = string.Empty;
            Assert.Throws<GalaxyException>(() => Customer.Create(fakeName));
        }
        [Fact]
        public void Create_customer_aggregateroot_true()
        {
            var createdNewCustomer = Customer.Create(this.GetFakeCustomerName);
            Assert.NotNull(createdNewCustomer); 
        }

        [Fact]
        public void Customer_aggregateroot_transient_true()
        {
            var customer = GetFakeCustomer;
            Assert.True(customer.IsTransient());
        }

        [Fact]
        public void Create_customer_event_fired()
        {
            var customer = GetFakeCustomer;
            Assert.True(customer.DomainEvents.Any());
        }
        
        private  string GetFakeCustomerName => $"Emre Yazıcı";
        private Customer GetFakeCustomer => Customer.Create(this.GetFakeCustomerName);
    }
}
