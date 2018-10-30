using CustomerSample.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerSample.Application.DomainEventHandlers
{
    public sealed class BrandCreatedDomainEventHandler : INotificationHandler<BrandCreatedDomainEvent>
    {
        public async Task Handle(BrandCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var newBrandName = notification.Brand.BrandName;
            await Task.FromResult(true);
        }
    }
}
