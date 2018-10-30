using Galaxy.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentSample.Domain.AggregatesModel.PaymentAggregate
{
    public class Money : ValueObject
    {

        public decimal? Amount { get; private set; }

        public int? CurrencyCode { get; private set; }
        private Money()
        {
        }

        public Money(decimal amount = default, int currencyCode = default) : this()
        {
            this.Amount = amount;
            this.CurrencyCode = currencyCode;
        }

        public void CalculateAmountForDolar()
        {
            // this Method  calculates amount for dolar
        }
        public void CalculateAmountForEuro()
        {
            // this Method  calculates amount for euro
        }


        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
            yield return CurrencyCode;
        }

    }
}
