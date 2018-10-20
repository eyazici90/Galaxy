
using Galaxy.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSample.Customer.Domain.AggregatesModel.BrandAggregate
{
    public interface IBrandRepository : ICustomRepository  
        // , Repository<Brand>  Cannot inherit from base repository cuz  we cannot give this class to  abilty of all generic methods
    {
        void Add(Brand brand);

        void Update(Brand brand);

        IQueryable<Brand> GetAllBrands();

        Task<Brand> GetBrandAggregate(int brandId);

        Task<Brand> GetAsync(int brandId);

        Task<IEnumerable<Merchant>> GetMerchantsByBrand(int brandId);

        // We could make a domain service for this policy injection
        void AddMerchantToBrand(Brand brand);
    }
}
