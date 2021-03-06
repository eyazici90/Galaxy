﻿using EventStoreSample.Domain.Exceptions;
using Galaxy.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventStoreSample.Domain.AggregatesModel.PaymentAggregate
{
    public sealed class PaymentTransactionState : AggregateRootEntityState<PaymentTransactionState, PaymentTransactionId>, ISnapshotable
    {
        public DateTime _transactionDateTime { get; private set; }

        public DateTime? _merchantTransactionDateTime { get; private set; }

        public string _msisdn { get; private set; }

        public string _description { get; private set; }

        public string _orderId { get; private set; }

        public Money _money { get; private set; }

        public int _transactionStatusId { get; private set; }

        public PaymenTransactionStatus _paymenTransactionStatus { get; private set; }

        public int _transactionTypeId { get; private set; }

        public PaymentTransactionType _paymentTransactionType { get; private set; }

        public List<PaymentTransactionDetailState> _paymentTransactionDetails { get; private set; }

        public IReadOnlyCollection<PaymentTransactionDetailState> PaymentTransactionDetails => _paymentTransactionDetails.AsReadOnly();

        private PaymentTransactionState()
        {
            RegisterEvent<Events.V1.TransactionCreatedDomainEvent>(When);
            RegisterEvent<Events.V1.TransactionAmountChangedDomainEvent>(When);
            RegisterEvent<Events.V1.TransactionStatusChangedDomainEvent>(When);
            RegisterEvent<Events.V1.TransactionDetailAssignedToTransactionDomainEvent>(When);
        }


        public PaymentTransactionState(string msisdn, string orderId, DateTime transactionDateTime) : this() =>
            EnsureValidState(msisdn, orderId, transactionDateTime);

        public bool EnsureValidState(string msisdn, string orderId, DateTime transactionDateTime)
        {
            if (string.IsNullOrWhiteSpace(msisdn)) { throw new InvalidEntityStateException(this, $"Invalid Msisdn"); }

            if (string.IsNullOrWhiteSpace(orderId)) { throw new InvalidEntityStateException(this, $"Invalid OrderId"); }

            if (DateTime.Now.AddDays(-1) > transactionDateTime) { throw new InvalidEntityStateException(this, $"TransactionDatetime cannot be higher than now"); }

            return true;
        }

        public void RestoreSnapshot(object snapshotState) =>
            With(this, state => 
            {
                var snapshot = (PaymentTransactionSnapshot)snapshotState;
                state.Id = snapshot.Id;
                state._transactionDateTime = snapshot.TransactionDateTime;
                state._merchantTransactionDateTime = snapshot.MerchantTransactionDateTime;
                state._msisdn = snapshot.Msisdn;
                state._description = snapshot.Description;
                state._orderId = snapshot.OrderId;
                state._money = Money.Create(Convert.ToDecimal(snapshot.Amount), 0);
                state._paymentTransactionDetails = snapshot.PaymentTransactionDetails.Select(d =>
                {
                    var detail = PaymentTransactionDetail.Create(Guid.Parse(d.PaymentTransactionStateId), d.Description);
                    return detail;
                }).ToList();
            });
       

        public object TakeSnapshot() => new PaymentTransactionSnapshot
        {
            Id = this.Id.ToString(),
            TransactionDateTime = this._transactionDateTime,
            MerchantTransactionDateTime = this._merchantTransactionDateTime,
            Msisdn = this._msisdn,
            Description = this._description,
            OrderId = this._orderId,
            Amount = this._money._amount,
            PaymentTransactionDetails = _paymentTransactionDetails.Select(d => new PaymentTransactionDetailSnapshot
            {
                PaymentTransactionStateId = d._paymentTransactionId.ToString(),
                Description = d._description
            })
        };

        private void When(Events.V1.TransactionCreatedDomainEvent @event) =>
            With(this, state =>
            {
                state.Id = @event.Id;

                state._msisdn = @event.Msisdn;

                state._orderId = @event.OrderId;

                state._transactionDateTime = @event.TransactionDateTime;

                state._transactionTypeId = PaymentTransactionType.DirectPaymentType.Id;

                state._paymentTransactionDetails = this._paymentTransactionDetails ?? new List<PaymentTransactionDetailState>();
            });



        private void When(Events.V1.TransactionAmountChangedDomainEvent @event) =>
            With(this, state => 
            {
                state._money = this._money ?? Money.Create(@event.Amount, 0);
            }); 

        private void When(Events.V1.TransactionStatusChangedDomainEvent @event) =>
            With(this, state =>
            {
                state._transactionStatusId = @event.TransactionStatusId;
            });


        private void When(Events.V1.TransactionDetailAssignedToTransactionDomainEvent @event) =>
            With(this, state =>
            {
                var detailState = PaymentTransactionDetail.Create(this.Id, @event.Description);
                detailState.ApplyEvent(@event);
                state._paymentTransactionDetails.Add(detailState);
            });
          

        public override string GetStreamName(string id) => $"PaymentTransaction-{id}";
    }
}
