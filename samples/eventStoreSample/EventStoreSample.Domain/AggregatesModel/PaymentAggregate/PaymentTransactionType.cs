using Galaxy.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventStoreSample.Domain.AggregatesModel.PaymentAggregate
{
    public class PaymentTransactionType : Entity
    {
        public static PaymentTransactionType DirectPaymentType = new PaymentTransactionType("DirectPayment").DirectPaymentTyped();
        public static PaymentTransactionType RefundPaymentType = new PaymentTransactionType("RefundPayment").RefundPaymentTyped();
        public static PaymentTransactionType ReservationPaymenType = new PaymentTransactionType("Reservation").ReservationPaymentTyped();


        public string _name { get; private set; }
        public string _description { get; private set; }
        private PaymentTransactionType()
        {
        }
        public PaymentTransactionType(string name, string desc = default)
        {
            this._name = !string.IsNullOrWhiteSpace(name) ? name
                                                       : throw new ArgumentNullException(nameof(name));
            this._description = desc;
        }
        private PaymentTransactionType DirectPaymentTyped()
        {
            this.Id = 1;
            return this;
        }
        private PaymentTransactionType RefundPaymentTyped()
        {
            this.Id = 2;
            return this;
        }
        private PaymentTransactionType ReservationPaymentTyped()
        {
            this.Id = 3;
            return this;
        }
    }
}
