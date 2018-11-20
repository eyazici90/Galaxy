using EventStoreSample.Domain.AggregatesModel.PaymentAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventStoreSample.IntegrationEvents
{

    public class TransactionCreatedIntegrationEvent
    {
        public PaymentTransaction paymentTransaction { get; private set; }
        
        public TransactionCreatedIntegrationEvent(PaymentTransaction transaction)
        {
            this.paymentTransaction = transaction;
        }
    }
}
