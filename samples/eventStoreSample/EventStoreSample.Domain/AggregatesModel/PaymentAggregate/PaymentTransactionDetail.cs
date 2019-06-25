using System;
using System.Collections.Generic;
using System.Text;

namespace EventStoreSample.Domain.AggregatesModel.PaymentAggregate
{
    public static class PaymentTransactionDetail
    {
        public static PaymentTransactionDetailState Create(Guid paymentTransactionStateId, string description)
        {
            var state = new PaymentTransactionDetailState(paymentTransactionStateId, description);
            state.ApplyEvent(new Events.V1.TransactionDetailCreatedDomainEvent(paymentTransactionStateId, description));
            return state;
        }
    }
}
