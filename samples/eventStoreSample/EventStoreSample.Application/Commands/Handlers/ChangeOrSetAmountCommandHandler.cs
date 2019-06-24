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
    public sealed class ChangeOrSetAmountCommandHandler : CommandHandlerBase<PaymentTransaction, object, Guid>
        , IRequestHandler<ChangeOrSetAmountCommand, bool>
    {
        public ChangeOrSetAmountCommandHandler(IUnitOfWorkAsync unitOfWorkAsync
            , IRepositoryAsync<PaymentTransaction, Guid> aggregateRootRepository
            , IObjectMapper objectMapper) : base(unitOfWorkAsync, aggregateRootRepository, objectMapper)
        {
        }

        public async Task<bool> Handle(ChangeOrSetAmountCommand request, CancellationToken cancellationToken)
        {
            await UpdateAsync(Guid.Parse(request.Id), async payment =>
            {
                payment.SomeNotNull()
                    .Match(p => p.ChangeOrSetAmountTo(
                        Money.Create(request.Amount.Value, request.CurrencyCode.Value)
                    ), () => throw new AggregateNotFoundException());
            });

            return await Task.FromResult(true);
        }
    }
}
