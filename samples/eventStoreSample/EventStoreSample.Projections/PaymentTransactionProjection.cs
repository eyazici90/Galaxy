
using EventStoreSample.Domain.AggregatesModel.PaymentAggregate;
using Galaxy.EventStore;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventStoreSample.Projections
{
    public class PaymentTransactionProjection : ProjectionHandler
    { 

        public override async Task Handle(object e)
        { 
            switch (e)
            {
                case Events.V1.TransactionCreatedDomainEvent t: 
                    Console.WriteLine($"{DateTime.Now} - Projected  event {typeof(Events.V1.TransactionCreatedDomainEvent)} msiSdn : {t.Msisdn}");
                    break;

                case Events.V1.TransactionAmountChangedDomainEvent t:
                    Console.WriteLine($"{DateTime.Now} - Projected  event {typeof(Events.V1.TransactionAmountChangedDomainEvent)} amount : {t.Amount}");
                    break;

                case Events.V1.TransactionDetailAssignedToTransactionDomainEvent t:
                    Console.WriteLine($"{DateTime.Now} - Projected  event {typeof(Events.V1.TransactionDetailAssignedToTransactionDomainEvent)} desc : {t.Description}");
                    break;
                    
            }

        }
    }
}
