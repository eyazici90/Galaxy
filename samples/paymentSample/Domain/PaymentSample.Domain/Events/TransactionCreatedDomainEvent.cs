using MediatR;
using PaymentSample.Domain.AggregatesModel.PaymentAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentSample.Domain.Events
{
    public class TransactionCreatedDomainEvent
    : INotification
    {
        public PaymentTransaction PaymentTransaction { get; private set; }


        public TransactionCreatedDomainEvent(PaymentTransaction transaction)
        {
            this.PaymentTransaction = transaction;
        }
    }
}
