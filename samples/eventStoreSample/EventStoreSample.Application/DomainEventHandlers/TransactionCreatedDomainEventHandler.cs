using EventStoreSample.Domain.Events;
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
        public async Task Handle(TransactionCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var aggregateRoot = notification.paymentTransaction;

        }
    }
}
