using EventStoreSample.Domain.Events;
using EventStoreSample.Domain.Exceptions;
using Galaxy.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventStoreSample.Domain.AggregatesModel.PaymentAggregate
{
    public sealed class PaymentTransaction : AggregateRootEntity<Guid>, ISnapshotable
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
            RegisterEvent<TransactionCreatedDomainEvent>(When);
            RegisterEvent<TransactionAmountChangedDomainEvent>(When);
            RegisterEvent<TransactionStatusChangedDomainEvent>(When);
        }

        private PaymentTransaction(string msisdn, string orderId, DateTime transactionDateTime) : this()
        {
            this.Msisdn = !string.IsNullOrWhiteSpace(msisdn) ? msisdn
                                                   : throw new ArgumentNullException(nameof(msisdn));
            this.OrderId = !string.IsNullOrWhiteSpace(orderId) ? orderId
                                                     : throw new ArgumentNullException(nameof(orderId));

            if (DateTime.Now.AddDays(-1) > transactionDateTime)
                throw new PaymentDomainException($"Invalid transactionDateTime {transactionDateTime}");
            
            ApplyEvent(new TransactionCreatedDomainEvent(msisdn, orderId, transactionDateTime));
        }

        public static PaymentTransaction Create(string msisdn, string orderId, DateTime transactionDateTime)
        {
            return new PaymentTransaction(msisdn, orderId, transactionDateTime);
        }

        public void RestoreSnapshot(object state)
        {
            var snapshot = (PaymentTransactionSnapshot)state;

            TransactionDateTime = snapshot.TransactionDateTime;
            MerchantTransactionDateTime = snapshot.MerchantTransactionDateTime;
            Msisdn = snapshot.Msisdn;
            Description = snapshot.Description;
            OrderId = snapshot.OrderId;
        }

        public object TakeSnapshot() => new PaymentTransactionSnapshot
        {
            TransactionDateTime = this.TransactionDateTime,
            MerchantTransactionDateTime = this.MerchantTransactionDateTime,
            Msisdn = this. Msisdn,
            Description = this.Description,
            OrderId = this.OrderId
        };

        private void When(TransactionCreatedDomainEvent @event)
        {
            this.TransactionDateTime = @event.TransactionDateTime;

            this.TransactionTypeId = PaymentTransactionType.DirectPaymentType.Id;
        }

        private void When(TransactionAmountChangedDomainEvent @event)
        {
            this.Money = @event.Money;
        }

        private void When(TransactionStatusChangedDomainEvent @event)
        {
            this.TransactionStatusId = @event.TransactionStatusId;
        }
        
        public PaymentTransaction RefundPaymentTyped()
        {
            this.TransactionTypeId = PaymentTransactionType.RefundPaymentType.Id;
            return this;
        }

        public Money SetMoney(int currencyCode, decimal amount)
        {
            var money = Money.Create(amount, currencyCode);
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
            // AggregateRoot leads all owned domain events !!!
            ApplyEvent(new TransactionAmountChangedDomainEvent(money));
        }

        public void PaymentStatusSucceded()
        {
            ApplyEvent(new TransactionStatusChangedDomainEvent(PaymenTransactionStatus.SuccessStatus.Id));
        }

        public void PaymentStatusFailed()
        {
            ApplyEvent(new TransactionStatusChangedDomainEvent(PaymenTransactionStatus.FailStatus.Id));
        } 
    }
}
