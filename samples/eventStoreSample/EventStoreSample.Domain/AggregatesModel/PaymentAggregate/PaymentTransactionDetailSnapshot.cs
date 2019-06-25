using System;
using System.Collections.Generic;
using System.Text;

namespace EventStoreSample.Domain.AggregatesModel.PaymentAggregate
{
    public class PaymentTransactionDetailSnapshot
    {
        public string PaymentTransactionStateId { get;  set; }
        public string Description { get;  set; }
    }
}
