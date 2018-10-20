using Galaxy.Domain;
using Galaxy.Tests.Domain.AggregatesModel.CustomerAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Galaxy.Tests.Domain
{
    public class EntityHelperTests
    {
        [Fact]
        public void Get_primary_key_type_tests()
        {
            Assert.Equal(typeof(int)
                , EntityHelper.GetPrimaryKeyType<Customer>());

            Assert.Equal(typeof(int), EntityHelper.GetPrimaryKeyType(typeof(Customer)));

            Assert.Equal(typeof(long), EntityHelper.GetPrimaryKeyType<Brand>());
        }
    }
}
