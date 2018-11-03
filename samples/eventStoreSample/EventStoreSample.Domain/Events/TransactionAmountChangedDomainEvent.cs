using EventStoreSample.Domain.AggregatesModel.PaymentAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventStoreSample.Domain.Events
{
    public class TransactionAmountChangedDomainEvent
     : INotification
    {
        public PaymentTransaction PaymentTransaction { get; private set; }


        public TransactionAmountChangedDomainEvent(PaymentTransaction paymentTransaction)
        {
            this.PaymentTransaction = paymentTransaction;
        }
    }
}
