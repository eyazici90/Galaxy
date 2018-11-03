using EventStoreSample.Domain.AggregatesModel.PaymentAggregate;
using Galaxy.Commands;
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
    
    public class RefundPaymentCommandHandler : CommandHandlerBase<PaymentTransaction, Guid>
        , IRequestHandler<RefundPaymentCommand, bool>
    {
        public RefundPaymentCommandHandler(IUnitOfWorkAsync unitOfWorkAsync
            , IRepositoryAsync<PaymentTransaction, Guid> aggregateRootRepository) : base(unitOfWorkAsync, aggregateRootRepository)
        {
        }

        public async Task<bool> Handle(RefundPaymentCommand request, CancellationToken cancellationToken)
        {
            await AddAsync(async () =>
            {
                var paymentTransaction = PaymentTransaction.Create(request.Msisdn, request.OrderId, DateTime.Now);

                paymentTransaction
                    .RefundPaymentTyped()
                    .SetMoney(request.CurrencyCode.Value, request.Amount.Value);

                return paymentTransaction;
            });

            return await Task.FromResult(true);
        }
    }
}
