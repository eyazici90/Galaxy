using Galaxy.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentSample.Domain.AggregatesModel.PaymentAggregate
{
   public class PaymenTransactionStatus : Entity
    {
        public static PaymenTransactionStatus SuccessStatus = new PaymenTransactionStatus("Success").StatusSuccedeed();
        public static PaymenTransactionStatus FailStatus = new PaymenTransactionStatus("Fail").StatusFailed();

        public string Name { get; private set; }

        private PaymenTransactionStatus()
        {
        }
        public PaymenTransactionStatus(string name)
        {
            this.Name = !string.IsNullOrWhiteSpace(name) ? name
                                                         : throw new ArgumentNullException(nameof(name));
        }
        private PaymenTransactionStatus StatusSuccedeed()
        {
            this.Id = 1;
            return this;
        }
        private PaymenTransactionStatus StatusFailed()
        {
            this.Id = 2;
            return this;
        }
    }
}
