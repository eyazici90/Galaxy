using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventStoreSample.Application.Commands
{
   public sealed class ChangeOrSetAmountCommand : IRequest<bool>
    {
        public string Id { get; set; }

        public decimal? Amount { get; set; }

        public int? CurrencyCode { get; set; }

        public string Msisdn { get; set; }

    }
}
