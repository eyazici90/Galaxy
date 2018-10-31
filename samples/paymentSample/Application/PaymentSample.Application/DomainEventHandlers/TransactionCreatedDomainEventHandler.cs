
using Galaxy.Log;
using MediatR;
using PaymentSample.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentSample.Application.DomainEventHandlers
{
    public class TransactionCreatedDomainEventHandler : INotificationHandler<TransactionCreatedDomainEvent>
    {
        private readonly ILog _log;
        public TransactionCreatedDomainEventHandler(ILog log)
        {
            _log = log;
        }

        public async Task Handle(TransactionCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            this._log.Information($"{nameof(TransactionCreatedDomainEvent)} happened. value : {notification.PaymentTransaction.Id}");
        }
    }
}
