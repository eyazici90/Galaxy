using EventStoreSample.Domain.AggregatesModel.PaymentAggregate;
using Galaxy.Commands;
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

    public class RefundPaymentCommandHandler : CommandHandlerBase<PaymentTransactionState, object, Guid>
      , IRequestHandler<RefundPaymentCommand, bool>
    {
        public RefundPaymentCommandHandler(IUnitOfWorkAsync unitOfWorkAsync
            , IRepositoryAsync<PaymentTransactionState, Guid> aggregateRootRepository
            , IObjectMapper objectMapper) : base(unitOfWorkAsync, aggregateRootRepository, objectMapper)
        {
        }

        public async Task<bool> Handle(RefundPaymentCommand request, CancellationToken cancellationToken)
        {
            await AddAsync(async () =>
            {
                var state = PaymentTransaction.Create(Guid.NewGuid().ToString(), request.Msisdn, request.OrderId, DateTime.Now);

                PaymentTransaction
                    .SetMoney(state, request.Amount.Value);

                return state;
            });

            return await Task.FromResult(true);
        }
    }
}
