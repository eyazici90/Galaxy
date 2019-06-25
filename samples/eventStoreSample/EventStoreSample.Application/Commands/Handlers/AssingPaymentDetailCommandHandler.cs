using EventStoreSample.Domain.AggregatesModel.PaymentAggregate;
using Galaxy.Commands;
using Galaxy.EventStore;
using Galaxy.ObjectMapping;
using Galaxy.Repositories;
using Galaxy.UnitOfWork;
using MediatR;
using Optional;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventStoreSample.Application.Commands.Handlers
{ 
    public class AssingPaymentDetailCommandHandler : CommandHandlerBase<PaymentTransactionState, object, Guid>
                , IRequestHandler<AssingPaymentDetailCommand, bool>
    {
        public AssingPaymentDetailCommandHandler(IUnitOfWorkAsync unitOfWorkAsync
            , IRepositoryAsync<PaymentTransactionState, Guid> aggregateRootRepository
            , IObjectMapper objectMapper) : base(unitOfWorkAsync, aggregateRootRepository, objectMapper)
        {
        }

        public async Task<bool> Handle(AssingPaymentDetailCommand request, CancellationToken cancellationToken)
        {
            await UpdateAsync(Guid.Parse(request.PaymentTransactionStateId), async payment =>
            {
                payment.SomeNotNull()
                    .Match(p => PaymentTransaction.AssignDetail(p, request.Description)
                    , () => throw new AggregateNotFoundException());
            });

            return await Task.FromResult(true);
        }
    }
}
