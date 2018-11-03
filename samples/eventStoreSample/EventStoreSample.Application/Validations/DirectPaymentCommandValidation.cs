using EventStoreSample.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventStoreSample.Application.Validations
{
    
    public class DirectPaymentCommandValidation : AbstractValidator<DirectPaymentCommand>
    {
        public DirectPaymentCommandValidation()
        {
            RuleFor(t => t.Msisdn).NotEmpty().MinimumLength(8);
        }
    }
}
