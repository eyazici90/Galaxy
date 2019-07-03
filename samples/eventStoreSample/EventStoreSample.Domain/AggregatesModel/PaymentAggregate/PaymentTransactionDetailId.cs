using System;
using System.Collections.Generic;
using System.Text;

namespace EventStoreSample.Domain.AggregatesModel.PaymentAggregate
{ 
    public class PaymentTransactionDetailId
    {
        public readonly string Id;
        public PaymentTransactionDetailId(string id) => Id = id;
        public override string ToString() => Id;

        public static implicit operator string(PaymentTransactionDetailId self) => self.Id;

        public static implicit operator Guid(PaymentTransactionDetailId self) => Guid.Parse(self.Id);

        public static implicit operator PaymentTransactionDetailId(string value) => new PaymentTransactionDetailId(value);

    }
}
