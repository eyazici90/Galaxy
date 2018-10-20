
using CustomerSample.Customer.Domain.Exceptions;
using Galaxy.Domain;
using System;
using System.Threading.Tasks;

namespace CustomerSample.Customer.Domain.AggregatesModel.BrandAggregate
{
   public class Merchant : Entity
    {
        
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public int BrandId { get; private set; }
      //  public Brand MyProperty { get; set; }
        public string Gsm { get; private set; }
        public bool IsActive { get; private set; }
        public string Vkn { get; private set; }
        public bool IsFraud { get; private set; }

        // Important :: Relations between aggragetes only with their uniq Id 's. No navigation property for different aggregates !!!
        // public Limit Limit { get; private set; }

        public int LimitId { get; private set; }

        private Merchant(){ }
        public Merchant(string name, string surname , int brandId, int limitId, string gsm = default)
        {
            this.Name = !string.IsNullOrWhiteSpace(name) ? name
                                                         : throw new ArgumentNullException(nameof(name));
            this.Surname = !string.IsNullOrWhiteSpace(surname) ? surname
                                                               : throw new ArgumentNullException(nameof(surname));
            this.BrandId = brandId;
            this.LimitId = limitId;
        }

        public static Merchant Create(string name, string surname, int brandId, int limitId, string gsm = default)
        {
            return new Merchant(name, surname, brandId, limitId, gsm);
        }

        public void ActivateMerchant() {
            this.IsActive = true;
        }

        public  void DeactivateMerchant(){
            this.IsActive = false;
        }

        public async Task<int> ChangeBrand(int brandId, IBrandPolicy policy) {
            if (policy == null)
                throw new ArgumentNullException(nameof(policy));
            var result = await policy.CheckAssignment(brandId);
            if (!result)
            {
                throw new CustomerDomainException($"{brandId} Brand is Invalid or in Blacklist cannot be assigned!!! ");
            }
            this.BrandId = brandId;
            return this.BrandId;
        }

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            this.Name = name;
        }
        public void ChangeSurname(string surname)
        {
            if (string.IsNullOrWhiteSpace(surname))
                throw new ArgumentNullException(nameof(surname));
            
            this.Surname = surname;
        }

        public void ChangeOrAddVkn(string vkn)
        {
            if (string.IsNullOrWhiteSpace(vkn))
                throw new ArgumentNullException(nameof(vkn));
            if (vkn.Length != 11)
                throw new CustomerDomainException($"Invalid Vkn number of length : {vkn}");
            // Regex validations could be here.

            this.Vkn = vkn;

        }

        public string GetFullName => $"{this.Name} {this.Surname}";
    }
}
