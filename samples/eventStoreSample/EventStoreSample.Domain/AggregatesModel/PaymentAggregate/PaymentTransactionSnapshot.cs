using System;
using System.Collections.Generic;
using System.Text;

namespace EventStoreSample.Domain.AggregatesModel.PaymentAggregate
{
    public class PaymentTransactionSnapshot
    {
        public string Id { get; set; }
        public DateTime TransactionDateTime { get;  set; }

        public DateTime? MerchantTransactionDateTime { get;  set; }

        public string Msisdn { get;  set; }

        public decimal? Amount { get; set; }

        public string Description { get;  set; }

        public string OrderId { get;  set; }

        public IEnumerable<PaymentTransactionDetailSnapshot> PaymentTransactionDetails { get; set; }
    }
}
