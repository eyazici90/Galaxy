using EventStoreSample.IntegrationEvents;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventStoreSample.Listener.Consumers
{
    
    public class TransactionCreatedConsumer :  IConsumer<TransactionCreatedIntegrationEvent>
    {

        public async Task Consume(ConsumeContext<TransactionCreatedIntegrationEvent> context)
        {
            var message = context.Message;
        }
    }
}
