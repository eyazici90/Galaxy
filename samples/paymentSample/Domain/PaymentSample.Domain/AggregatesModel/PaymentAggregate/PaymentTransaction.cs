using Galaxy.Domain;
using PaymentSample.Domain.Events;
using PaymentSample.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentSample.Domain.AggregatesModel.PaymentAggregate
{
   public sealed class PaymentTransaction : AggregateRootEntity<Guid>
    {
        public Money Money { get; private set; }

        public DateTime TransactionDateTime { get; private set; }

        public DateTime? MerchantTransactionDateTime { get; private set; }

        public string Msisdn { get; private set; }

        public string Description { get; private set; }

        public string OrderId { get; private set; }

        public string ResponseCode { get; private set; }

        public string ResponseMessage { get; private set; }

        public int TransactionStatusId { get; private set; }
        public PaymenTransactionStatus PaymenTransactionStatus { get; private set; }

        public int TransactionTypeId { get; private set; }

        public PaymentTransactionType PaymentTransactionType { get; private set; }

        public int? ReferanceTransactionId { get; private set; }

        public PaymentTransaction ReferanceTransaction { get; private set; }

        private PaymentTransaction()
        {
        }

        private PaymentTransaction(string msisdn, string orderId, DateTime transactionDateTime) : this()
        {
            this.Msisdn = !string.IsNullOrWhiteSpace(msisdn) ? msisdn
                                                      : throw new ArgumentNullException(nameof(msisdn));
            this.OrderId = !string.IsNullOrWhiteSpace(orderId) ? orderId
                                                     : throw new ArgumentNullException(nameof(orderId));

            this.TransactionDateTime = transactionDateTime;
            
            this.TransactionTypeId = PaymentTransactionType.DirectPaymentType.Id;
            
            if (DateTime.Now.AddDays(-1) > transactionDateTime)
            {
                throw new PaymentDomainException($"Invalid transactionDateTime {transactionDateTime}");
            } 

            AddDomainEvent(new TransactionCreatedDomainEvent(this));
        }

        public static PaymentTransaction Create(string msisdn, string orderId, DateTime transactionDateTime)
        {
            return new PaymentTransaction(msisdn, orderId, transactionDateTime);
        }

        public Money SetMoney(int currencyCode, decimal amount)
        {
            var money = Money.Create(amount,currencyCode);
            this.Money = money;
            return this.Money;
        }
        
        public void ChangeOrSetAmountTo(Money money)
        {
            // Max Daily Amount. Could get from environment!
            if (money.Amount > 1000)
            {
                throw new PaymentDomainException($"Max daily amount exceed for this transaction {this.Id}");
            }
            this.Money = money;
            // AggregateRoot leads all owned domain events !!!
            AddDomainEvent(new TransactionAmountChangedDomainEvent(this));
        }

        public void PaymentStatusSucceded()
        {
            this.TransactionStatusId = PaymenTransactionStatus.SuccessStatus.Id;
            AddDomainEvent(new TransactionStatusChangedDomainEvent(this));

        }

        public void PaymentStatusFailed()
        {
            this.TransactionStatusId = PaymenTransactionStatus.FailStatus.Id;
            AddDomainEvent(new TransactionStatusChangedDomainEvent(this));
        }

    }
}
