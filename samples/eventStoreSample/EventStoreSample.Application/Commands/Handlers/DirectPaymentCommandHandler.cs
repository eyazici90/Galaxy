using EventStoreSample.Domain.AggregatesModel.PaymentAggregate;
using Galaxy.Commands;
using Galaxy.Infrastructure;
using Galaxy.ObjectMapping;
using Galaxy.Repositories;
using Galaxy.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventStoreSample.Application.Commands.Handlers
{
    public class DirectPaymentCommandHandler : CommandHandlerBase<PaymentTransactionState, object, Guid>
         , IRequestHandler<DirectPaymentCommand, bool>
    {
        public DirectPaymentCommandHandler(IUnitOfWorkAsync unitOfWorkAsync
            , IRepositoryAsync<PaymentTransactionState, Guid> aggregateRootRepository
            , IObjectMapper objectMapper) : base(unitOfWorkAsync, aggregateRootRepository, objectMapper)
        {
        }

        public async Task<bool> Handle(DirectPaymentCommand request, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid();
            await AddAsync(async () =>
            {
                var state = PaymentTransaction.Create(id.ToString(), request.Msisdn, request.OrderId, DateTime.Now);
                PaymentTransaction.SetMoney(state, request.Amount.Value);
                return state;
            }, id);

            return await Task.FromResult(true);
        }
    }
}
