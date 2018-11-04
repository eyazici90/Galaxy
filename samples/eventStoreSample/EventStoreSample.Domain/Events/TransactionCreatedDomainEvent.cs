using EventStoreSample.Domain.AggregatesModel.PaymentAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventStoreSample.Domain.Events
{
    public class TransactionCreatedDomainEvent: INotification
    {
        public PaymentTransaction paymentTransaction { get; private set; }


        public TransactionCreatedDomainEvent(PaymentTransaction transaction)
        {
            this.paymentTransaction = transaction;
        }
    }
}
