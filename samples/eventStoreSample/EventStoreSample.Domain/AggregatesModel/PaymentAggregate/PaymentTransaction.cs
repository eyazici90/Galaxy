
using EventStoreSample.Domain.Exceptions;
using EventStoreSample.Domain.Extensions;
using Galaxy.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventStoreSample.Domain.AggregatesModel.PaymentAggregate
{
    public static class PaymentTransaction
    {
        public static PaymentTransactionState Create(string id, string msisdn, string orderId, DateTime transactionDateTime) =>
           StateFactory.Create(() => new PaymentTransactionState(msisdn, orderId, transactionDateTime)
               , state => state.ApplyEvent(new Events.V1.TransactionCreatedDomainEvent(id, msisdn, orderId, transactionDateTime)));

        public static void ChangeOrSetAmountTo(PaymentTransactionState state, Money money)
        {
            if (money._amount > 1000) { throw new DailyAmountExceedException(state.Id.ToString()); }
            state.ApplyEvent(new Events.V1.TransactionAmountChangedDomainEvent(money._amount.Value));
        }

        public static void PaymentStatusSucceded(PaymentTransactionState state) =>
            state.ApplyEvent(new Events.V1.TransactionStatusChangedDomainEvent(PaymenTransactionStatus.SuccessStatus.Id));

        public static void PaymentStatusFailed(PaymentTransactionState state) =>
            state.ApplyEvent(new Events.V1.TransactionStatusChangedDomainEvent(PaymenTransactionStatus.FailStatus.Id));

        public static void SetMoney(PaymentTransactionState state, decimal amount) =>
            state.ApplyEvent(new Events.V1.TransactionAmountChangedDomainEvent(amount));

        public static void AssignDetail(PaymentTransactionState state, string description) =>
           state.ApplyEvent(new Events.V1.TransactionDetailAssignedToTransactionDomainEvent(state.Id.ToString(), description));
    }
}
