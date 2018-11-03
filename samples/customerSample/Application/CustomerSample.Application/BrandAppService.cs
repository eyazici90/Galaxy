using CustomerSample.Common.Dtos;
using CustomerSample.Customer.Domain.AggregatesModel.BrandAggregate;
using Galaxy.Application;
using Galaxy.Cache;
using Galaxy.ObjectMapping;
using Galaxy.Repositories;
using Galaxy.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSample.Application
{
    public class BrandAppService : CrudAppServiceAsync<BrandDto, int, Brand>
    {
        public BrandAppService(IRepositoryAsync<Brand, int> repositoryAsync
            , IObjectMapper objectMapper
            , IUnitOfWorkAsync unitOfWork) : base(repositoryAsync, objectMapper, unitOfWork)
        {
        }
        
        public async Task<BrandDto> GetBrandByIdAsync(int brandId) =>
             await FindAsync(brandId);

        public async Task<IList<BrandDto>> GetAllBrandsAsync() => 
             await this.QueryableNoTrack().ToListAsync();

        public async Task<BrandDto> AddNewBrandAsync(BrandDto brandDto)
        {
            return await AddAsync(async () => {
                var brand = Brand.Create(brandDto.EMail, brandDto.BrandName, brandDto.Gsm, brandDto.SNCode);
                return brand;
            });
        }

        public async Task<BrandDto> ChangeBrandNameAsync(BrandDto brandDto)
        {
            return await UpdateAsync(brandDto.Id, async brand => {
                brand
                   .ChangeBrandName(brandDto.BrandName);
            });
        }

        public  BrandDto AddNewBrand(BrandDto brandDto)
        {
           return Add( () => {
               return Brand
                 .Create(brandDto.EMail, brandDto.BrandName, brandDto.Gsm, brandDto.SNCode);
            });
            
        }

        public BrandDto ChangeBrandName(BrandDto brandDto)
        {
            return Update(brandDto.Id, brand => {
                brand
                   .ChangeBrandName(brandDto.BrandName);
            });
        }

     
    }
}
