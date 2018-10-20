
using Galaxy.Domain;
using System; 

namespace CustomerSample.Domain.AggregatesModel.LimitationAggregate
{
    public sealed class Limit : AggregateRootEntity
    {
        

        public  LimitType LimitType { get; private set; }

        public int LimitTypeId { get; private set; }

        public  decimal LimitValue { get; private set; }

        public  string LimitName { get; private set; }

        public  bool IsActive { get; private set; }

        private Limit(){}


        public Limit(string name,decimal value, int typeId ) : this()
        {
            this.LimitName = !string.IsNullOrWhiteSpace(name) ? name
                                                              : throw new ArgumentNullException(nameof(name));
            this.LimitValue = value ;
            this.LimitTypeId = typeId ;

        }
        public void ActivateLimit()
        {
            this.IsActive = true;
        }
        public void DeactivateLimit()
        {
            this.IsActive = false;
        }

        public DateTime GetExpirationDate() => this.LimitType.EndDate;
        
            
        
    }
}
