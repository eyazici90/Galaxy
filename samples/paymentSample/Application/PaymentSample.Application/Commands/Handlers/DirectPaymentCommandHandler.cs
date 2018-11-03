using Galaxy.Commands;
using Galaxy.Infrastructure;
using Galaxy.Repositories;
using Galaxy.UnitOfWork;
using MediatR;
using PaymentSample.Domain.AggregatesModel.PaymentAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentSample.Application.Commands.Handlers
{
    public class DirectPaymentCommandHandler : CommandHandlerBase<PaymentTransaction,int>
        , IRequestHandler<DirectPaymentCommand, bool>
    {
        public DirectPaymentCommandHandler(IUnitOfWorkAsync unitOfWorkAsync
            , IRepositoryAsync<PaymentTransaction,int> aggregateRootRepository) : base(unitOfWorkAsync, aggregateRootRepository)
        {
        }

        public async Task<bool> Handle(DirectPaymentCommand request, CancellationToken cancellationToken)
        {
           await AddAsync(async () =>
            {
                var paymentTransaction = PaymentTransaction.Create(request.Msisdn, request.OrderId, DateTime.Now);

                paymentTransaction
                    .SetMoney(request.CurrencyCode.Value, request.Amount.Value)
                    .SyncObjectState(ObjectState.Added);

                return paymentTransaction;
            });

            return await Task.FromResult(true);
        }
    }
}
