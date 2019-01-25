
using CustomerSample.Customer.Domain.AggregatesModel.BrandAggregate;
using Galaxy.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSample.Customer.Domain.EFRepositories.BrandAggregate
{
    public class BrandRepository : IBrandRepository
    {
        private readonly IRepositoryAsync<Brand> _brandRep;
        public BrandRepository(IRepositoryAsync<Brand> brandRep)
        {
            this._brandRep = brandRep ?? throw new ArgumentNullException(nameof(brandRep));
        }
        public void Add(Brand brand)
        {
            this._brandRep.Insert(brand);
        }

    
        public void AddMerchantToBrand(Brand brand)
        {
            // Brand is AggregateRoot and adding merchant to its collection can explain as simply updateing brand
            this._brandRep.Update(brand);
        }
        public async Task<Brand> GetBrandAggregate(int brandId) {
            return await this._brandRep.Queryable()
                    .Include(m=>m.Merchants)
                    .SingleOrDefaultAsync(b=>b.Id == brandId);
        }
        public async Task<Brand> GetAsync(int brandId)
        {
          
            return await this._brandRep.FindAsync(brandId);
        }

        public async Task<IEnumerable<Merchant>> GetMerchantsByBrand(int brandId)
        {
            return await this._brandRep.QueryableNoTrack()
                 .Include(m => m.Merchants)
                 .Where(b => b.Id == brandId)
                 .SelectMany(m=>m.Merchants)
                 .ToListAsync();
        }

        public void Update(Brand brand)
        {
            this._brandRep.Update(brand);
        }

        public  IQueryable<Brand> GetAllBrands()
        {
            return  this._brandRep.QueryableNoTrack();
        }
    }
}
