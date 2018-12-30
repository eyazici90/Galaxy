
using EventStoreSample.Domain.Events;
using EventStoreSample.IntegrationEvents;
using Galaxy.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventStoreSample.Application.DomainEventHandlers
{
    public class TransactionCreatedDomainEventHandler : INotificationHandler<TransactionCreatedDomainEvent>
    {
        private readonly IEventBus _eventBus;
        public TransactionCreatedDomainEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        public async Task Handle(TransactionCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var msiSdn = notification.Msisdn;
            var aggregateRoot = notification.Msisdn;
            await _eventBus.Publish(new TransactionCreatedIntegrationEvent(notification.TransactionDateTime, notification.Msisdn, notification.OrderId));
        }
    }
}
