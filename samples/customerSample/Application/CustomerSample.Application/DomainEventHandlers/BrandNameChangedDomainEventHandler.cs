using CustomerSample.Customer.Domain.AggregatesModel.BrandAggregate;
using CustomerSample.Domain.Events;
using Galaxy.Cache;
using Galaxy.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerSample.Application.DomainEventHandlers
{
    public sealed class BrandNameChangedDomainEventHandler : INotificationHandler<BrandNameChangedDomainEvent>
    {
        private readonly ICache _cacheServ;
        public BrandNameChangedDomainEventHandler(ICache cacheServ)
        {
            _cacheServ = cacheServ;
        }
        public async Task Handle(BrandNameChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            var newBrandName = notification.Brand.BrandName;
            await this._cacheServ.SetAsync(newBrandName, notification.Brand);
            await Task.FromResult(true);
        }
    }
}
