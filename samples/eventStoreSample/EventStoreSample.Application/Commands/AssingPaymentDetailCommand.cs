using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventStoreSample.Application.Commands
{ 
    public class AssingPaymentDetailCommand : IRequest<bool>
    {
        public string PaymentTransactionStateId { get; set; }
        public string Description { get; set; }
    }
}
