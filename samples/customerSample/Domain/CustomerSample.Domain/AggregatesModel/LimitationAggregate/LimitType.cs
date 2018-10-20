
using Galaxy.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerSample.Domain.AggregatesModel.LimitationAggregate
{
   public class LimitType : Entity , IEntity
    { 
        public string Name { get; set; }
        public string Description { get; private set; }
        public DateTime EndDate { get; private  set; }

        private LimitType(){}

        public LimitType(string name, DateTime endDate)
        {
            this.Name = !string.IsNullOrWhiteSpace(name) ? name
                                                         : throw new ArgumentNullException(nameof(name));
            this.EndDate = endDate;
        }


        public void  SetExpirationDate(DateTime endDate)
        {
            this.EndDate = EndDate;
        }
        public void SetDescription(string desc)
        {
            if (string.IsNullOrWhiteSpace(desc))
                throw new ArgumentNullException(nameof(desc));

            this.Description = desc;
        }
        public void SetName(string name )
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            this.Name = name; 
        }
    }
}
