using Galaxy.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentSample.Domain.AggregatesModel.PaymentAggregate
{
   public class PaymentTransactionType : Entity
    {
        public static PaymentTransactionType DirectPaymentType = new PaymentTransactionType("DirectPayment").DirectPaymentTyped();
        public static PaymentTransactionType RefundPaymentType = new PaymentTransactionType("RefundPayment").RefundPaymentTyped();
        public static PaymentTransactionType ReservationPaymenType = new PaymentTransactionType("Reservation").ReservationPaymentTyped();

        
        public string Name { get; private set; }
        public string Description { get; private set; }
        private PaymentTransactionType()
        {
        }
        public PaymentTransactionType(string name, string desc = default)
        {
            this.Name = !string.IsNullOrWhiteSpace(name) ? name
                                                       : throw new ArgumentNullException(nameof(name));
            this.Description = desc;
        }
        private PaymentTransactionType DirectPaymentTyped()
        {
            this._id = 1;
            return this;
        }
        private PaymentTransactionType RefundPaymentTyped()
        {
            this._id = 2;
            return this;
        }
        private PaymentTransactionType ReservationPaymentTyped()
        {
            this._id = 3;
            return this;
        }
    }
}
