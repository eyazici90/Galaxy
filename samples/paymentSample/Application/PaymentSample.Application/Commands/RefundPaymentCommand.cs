using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentSample.Application.Commands
{
    public class RefundPaymentCommand : IRequest<bool>
    {
        public decimal? Amount { get; set; }

        public int? CurrencyCode { get; set; }

        public string Msisdn { get; set; }

        public string OrderId { get; set; }
    }
}
