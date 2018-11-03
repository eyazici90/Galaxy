using EventStoreSample.Domain.AggregatesModel.PaymentAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventStoreSample.Domain.Events
{
    public class TransactionStatusChangedDomainEvent
     : INotification
    {
        public PaymentTransaction PaymentTransaction { get; private set; }


        public TransactionStatusChangedDomainEvent(PaymentTransaction transaction)
        {
            this.PaymentTransaction = transaction;
        }
    }
}
