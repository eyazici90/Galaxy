using EventStoreSample.Domain.AggregatesModel.PaymentAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventStoreSample.Domain.Events
{
    public class TransactionCreatedDomainEvent: INotification
    {
        public DateTime TransactionDateTime { get; private set; }

        public string Msisdn { get; private set; }
        
        public string OrderId { get; private set; }

        public TransactionCreatedDomainEvent(string msisdn, string orderId, DateTime transactionDateTime)
        {
            this.Msisdn = msisdn;
            this.OrderId = orderId;
            this.TransactionDateTime = transactionDateTime;
        }
    }
}
