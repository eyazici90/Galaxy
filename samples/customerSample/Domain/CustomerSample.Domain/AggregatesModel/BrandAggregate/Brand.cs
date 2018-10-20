
using CustomerSample.Customer.Domain.Exceptions;
using CustomerSample.Domain.Events;
using Galaxy.Auditing;
using Galaxy.Domain;
using Galaxy.Domain.Auditing;
using Galaxy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerSample.Customer.Domain.AggregatesModel.BrandAggregate
{
    public sealed class Brand : FullyAuditAggregateRootEntity
    {
        public  string EMail { get; private set; }
        public  string BrandName { get; private set; }
        public  string Gsm { get; private set; }
        public  string SNCode { get; private set; }
        public  bool IsActive { get; private set; }
        
        private List<Merchant> _merchants;

        public IEnumerable<Merchant> Merchants => _merchants.AsEnumerable();
        
        private Brand()
        {
            _merchants = new List<Merchant>();
        }

        public Brand(string email, string brandName, string gsm = default, string snCode = default) : this()
        {
            // AR Creation validations
            this.EMail = !string.IsNullOrWhiteSpace(email) ? email
                                                           : throw new ArgumentNullException(nameof(email));
            this.BrandName = !string.IsNullOrWhiteSpace(brandName) ? brandName
                                                                   : throw new ArgumentNullException(nameof(brandName));
        }

        public static Brand Create(string email, string brandName, string gsm = default, string snCode = default)
        {
            return new Brand( email,  brandName,  gsm ,  snCode );
        }

        public Brand ChangeBrandName(string brandName)
        {
            if (string.IsNullOrWhiteSpace(brandName))
                throw new ArgumentNullException(nameof(brandName));

            this.BrandName = brandName;
            AddDomainEvent(new BrandNameChangedDomainEvent(this));
            return this;
        }

        // It is not not named SetStatusId(int id). Aggregates methods must be domain conceptual ubiq language implementation !!!
        public Brand ActivateBrand()
        {
            this.IsActive = true;
            return this;
        }

        public Brand DeactivateBrand()
        {
            this.IsActive = false;
            return this;
        }

        public void ChangeOrAddGsm(string gsm)
        {
            if (string.IsNullOrWhiteSpace(gsm))
                throw new ArgumentNullException(nameof(gsm));

            if (gsm.Length > 11)
                throw new CustomerDomainException($"Invalid gsm number : {gsm}");

            if (gsm.StartsWith("-"))
                throw new CustomerDomainException($"Invalid gsm number with (-) : {gsm}");

            this.Gsm = gsm;
        }
        // Policy is a pattern. Conceptual rules known as Strategy design also. You could wrap this policy within Service also  
        public Merchant AddMerchant(string name, string surname, int brandId, int limitId
            , IBrandPolicy policy, string gsm = default)
        {
            if (policy == null)
                throw new ArgumentNullException(nameof(policy));

            if (this.Merchants.Count() >= policy.MaxAmountofMarchantperBrand)
            {
                throw new CustomerDomainException($"Maximum amount of Merchant exceed for this Brand !!! ");
            }

            var merchant = Merchant.Create(name, surname, brandId, limitId, gsm);
            _merchants.Add(merchant);
           return merchant;
        }

        public void ChangeMerchantName(string name, Merchant merchant, IBrandPolicy policy)
        {
            if (policy == null)
                throw new ArgumentNullException(nameof(policy));

            var existingMerchantforBrand = this._merchants.Where(x => x.BrandId == merchant.Id)
                                                          .SingleOrDefault();
            if (existingMerchantforBrand != null)
            {
                existingMerchantforBrand.ChangeName(merchant.Name);
                existingMerchantforBrand.SyncObjectState(ObjectState.Modified);
            }
            else
            {
                this.AddMerchant(merchant.Name, merchant.Surname, merchant.BrandId, merchant.LimitId, policy, merchant.Gsm);
            }

        }

        public Merchant ChangeOrAddVknToMerchant(int merchantId, string vkn) {

            if (merchantId == 0)
                throw new CustomerDomainException($"Invalid Merchant Id { merchantId}");

            var existingMerchant = this._merchants.SingleOrDefault(m=>m.Id == merchantId);
            if (existingMerchant == null)
                throw new CustomerDomainException($"There is no merchant with this id : {merchantId}");

            existingMerchant.ChangeOrAddVkn(vkn);
            return existingMerchant;
        }

        public void DeleteThisBrand()
        {
            // You cannot reach an entity object direcly if is no an aggregateroot 
            // So deleting an aggregate means you cannot reach its child objects automatically !!!
            //  builder.HasQueryFilter(p => !p.IsDeleted);
            // QueryFilter with EF Core. Auto including where clause thx to microsoft team :)
            

            this.IsDeleted = true;
        }

    }
}
