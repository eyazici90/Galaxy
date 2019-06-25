
using EventStoreSample.Domain.Exceptions;
using Galaxy.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventStoreSample.Domain.AggregatesModel.PaymentAggregate
{
    public sealed class PaymentTransaction : AggregateRootEntity<Guid>, ISnapshotable
    {
        public Money _money { get; private set; }

        public DateTime _transactionDateTime { get; private set; }

        public DateTime? _merchantTransactionDateTime { get; private set; }

        public string _msisdn { get; private set; }

        public string _description { get; private set; }

        public string _orderId { get; private set; }

        public int _transactionStatusId { get; private set; }

        public PaymenTransactionStatus _paymenTransactionStatus { get; private set; }

        public int _transactionTypeId { get; private set; }

        public PaymentTransactionType _paymentTransactionType { get; private set; } 

        private PaymentTransaction()
        {
            RegisterEvent<Events.V1.TransactionCreatedDomainEvent>(When);
            RegisterEvent<Events.V1.TransactionAmountChangedDomainEvent>(When);
            RegisterEvent<Events.V1.TransactionStatusChangedDomainEvent>(When);
        }

        private PaymentTransaction(string msisdn, string orderId, DateTime transactionDateTime) : this()
        {
            this._msisdn = !string.IsNullOrWhiteSpace(msisdn) ? msisdn
                                                   : throw new ArgumentNullException(nameof(msisdn));
            this._orderId = !string.IsNullOrWhiteSpace(orderId) ? orderId
                                                     : throw new ArgumentNullException(nameof(orderId));

            if (DateTime.Now.AddDays(-1) > transactionDateTime)
                throw new PaymentDomainException($"Invalid transactionDateTime {transactionDateTime}");
            
            ApplyEvent(new Events.V1.TransactionCreatedDomainEvent(msisdn, orderId, transactionDateTime));
        }

        public static PaymentTransaction Create(string msisdn, string orderId, DateTime transactionDateTime)
        {
            return new PaymentTransaction(msisdn, orderId, transactionDateTime);
        }

        public void RestoreSnapshot(object state)
        {
            var snapshot = (PaymentTransactionSnapshot)state;

            _transactionDateTime = snapshot.TransactionDateTime;
            _merchantTransactionDateTime = snapshot.MerchantTransactionDateTime;
            _msisdn = snapshot.Msisdn;
            _description = snapshot.Description;
            _orderId = snapshot.OrderId;
            _money  = SetMoney(0, Convert.ToDecimal(snapshot.Amount));
        }

        public object TakeSnapshot() => new PaymentTransactionSnapshot
        {
            TransactionDateTime = this._transactionDateTime,
            MerchantTransactionDateTime = this._merchantTransactionDateTime,
            Msisdn = this._msisdn,
            Description = this._description,
            OrderId = this._orderId,
            Amount = this._money._amount
        };

        private void When(Events.V1.TransactionCreatedDomainEvent @event)
        {
            this._msisdn = @event.Msisdn;

            this._orderId = @event.OrderId;

            this._transactionDateTime = @event.TransactionDateTime;

            this._transactionTypeId = PaymentTransactionType.DirectPaymentType.Id;
        }

        private void When(Events.V1.TransactionAmountChangedDomainEvent @event)
        {
            SetMoney(0, @event.Amount);
        }

        private void When(Events.V1.TransactionStatusChangedDomainEvent @event)
        {
            this._transactionStatusId = @event.TransactionStatusId;
        }
        
        public PaymentTransaction RefundPaymentTyped()
        {
            this._transactionTypeId = PaymentTransactionType.RefundPaymentType.Id;
            return this;
        }

        public Money SetMoney(int currencyCode, decimal amount)
        {
            var money = Money.Create(amount, currencyCode);
            this._money = money;
            return this._money;
        }

        public void ChangeOrSetAmountTo(Money money)
        { 
            if (money._amount > 1000)
            {
                throw new PaymentDomainException($"Max daily amount exceed for this transaction {this.Id}");
            } 
            ApplyEvent(new Events.V1.TransactionAmountChangedDomainEvent(money._amount.Value));
        }

        public void PaymentStatusSucceded()
        {
            ApplyEvent(new Events.V1.TransactionStatusChangedDomainEvent(PaymenTransactionStatus.SuccessStatus.Id));
        }

        public void PaymentStatusFailed()
        {
            ApplyEvent(new Events.V1.TransactionStatusChangedDomainEvent(PaymenTransactionStatus.FailStatus.Id));
        } 
    }
}
