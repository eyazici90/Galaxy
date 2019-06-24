using Galaxy.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventStoreSample.Domain.AggregatesModel.PaymentAggregate
{
    public class Money : ValueObject
    {

        public decimal? _amount { get; private set; }

        public int? _currencyCode { get; private set; }
        private Money()
        {
        }

        private Money(decimal amount = default, int currencyCode = default) : this()
        {
            this._amount = amount;
            this._currencyCode = currencyCode;
        }

        public static Money Create(decimal amount = default, int currencyCode = default)
        {
            return new Money(amount, currencyCode);
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
            yield return _amount;
            yield return _currencyCode;
        }

    }
}
