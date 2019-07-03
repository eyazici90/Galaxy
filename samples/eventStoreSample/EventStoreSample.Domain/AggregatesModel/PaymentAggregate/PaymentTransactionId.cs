using System;
using System.Collections.Generic;
using System.Text;

namespace EventStoreSample.Domain.AggregatesModel.PaymentAggregate
{ 
    public class PaymentTransactionId
    {
        public readonly string Id;
        public PaymentTransactionId(string id) => Id = id;
        public override string ToString() => Id;

        public static implicit operator string(PaymentTransactionId self) => self.Id;

        public static implicit operator Guid(PaymentTransactionId self) => Guid.Parse(self.Id);

        public static implicit operator PaymentTransactionId(string value) => new PaymentTransactionId(value);

    }
}
