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
        public int TransactionStatusId { get; private set; }

        public TransactionStatusChangedDomainEvent(int transactionStatusId)
        {
            this.TransactionStatusId = transactionStatusId;
        }
    }
}
