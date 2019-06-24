
using Galaxy.EventStore;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventStoreSample.Projections
{
    public class PaymentTransactionProjection : Projection  
    { 

        public override async Task Handle(object e)
        {
            Console.WriteLine($"{DateTime.Now} - Projected  event {e.GetType().FullName}");
        }
    }
}
