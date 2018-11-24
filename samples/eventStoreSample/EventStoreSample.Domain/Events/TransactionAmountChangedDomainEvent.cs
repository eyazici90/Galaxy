using EventStoreSample.Domain.AggregatesModel.PaymentAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventStoreSample.Domain.Events
{
    public class TransactionAmountChangedDomainEvent
     : INotification
    {
        public Money Money { get; private set; }

        public TransactionAmountChangedDomainEvent(Money money)
        {
            this.Money = money;
        }
    }
}
