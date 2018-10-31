using FluentValidation;
using PaymentSample.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentSample.Application.Validations
{
    public class PaymentTransactionValidation : AbstractValidator<DirectPaymentCommand>
    {
        public PaymentTransactionValidation()
        {
            RuleFor(t => t.Msisdn).NotEmpty().MinimumLength(8);
        }
    }
}
