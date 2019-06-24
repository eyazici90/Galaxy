
using EventStoreSample.Domain.AggregatesModel.PaymentAggregate;
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
    public class TransactionCreatedDomainEventHandler : INotificationHandler<Events.V1.TransactionCreatedDomainEvent>
    {
        private readonly IEventBus _eventBus;
        public TransactionCreatedDomainEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        public async Task Handle(Events.V1.TransactionCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var msiSdn = notification.Msisdn;
            var aggregateRoot = notification.Msisdn;
            await _eventBus.PublishAsync(new TransactionCreatedIntegrationEvent(notification.TransactionDateTime, notification.Msisdn, notification.OrderId));
        }
    }
}
