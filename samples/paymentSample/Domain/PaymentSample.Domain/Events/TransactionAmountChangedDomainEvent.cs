using MediatR;
using PaymentSample.Domain.AggregatesModel.PaymentAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentSample.Domain.Events
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
