using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventStoreSample.Domain.AggregatesModel.PaymentAggregate
{
    public static class Events
    {
        public static class V1
        {
            public class TransactionAmountChangedDomainEvent : INotification
            {
                public decimal Amount { get; private set; }

                public TransactionAmountChangedDomainEvent(decimal amount)
                {
                    this.Amount = amount;
                }
            }

            public class TransactionCreatedDomainEvent : INotification
            {
                public string Id { get; private set; }
                public DateTime TransactionDateTime { get; private set; }

                public string Msisdn { get; private set; }

                public string OrderId { get; private set; }

                public TransactionCreatedDomainEvent(string id , string msisdn, string orderId, DateTime transactionDateTime)
                {
                    this.Id = id;
                    this.Msisdn = msisdn;
                    this.OrderId = orderId;
                    this.TransactionDateTime = transactionDateTime;
                }
            }

            public class TransactionStatusChangedDomainEvent : INotification
            {
                public int TransactionStatusId { get; private set; }

                public TransactionStatusChangedDomainEvent(int transactionStatusId)
                {
                    this.TransactionStatusId = transactionStatusId;
                }
            }
            public class TransactionDetailCreatedDomainEvent : INotification
            {
                public Guid PaymentTransactionId { get; private set; }
                public string Description{ get; private set; }
                public TransactionDetailCreatedDomainEvent(Guid paymentTransactionId, string description)
                {
                    this.Description = description;
                    this.PaymentTransactionId = paymentTransactionId; 
                }
            }

            public class TransactionDetailAssignedToTransactionDomainEvent : INotification
            {
                public string Description { get; private set; }
                public TransactionDetailAssignedToTransactionDomainEvent(string description)
                {
                    this.Description = description;
                }
            }
        }
    }
}
