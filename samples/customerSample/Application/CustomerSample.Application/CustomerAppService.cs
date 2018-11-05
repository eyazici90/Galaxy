
using CustomerSample.Application.Abstractions;
using CustomerSample.Common.Dtos;
using CustomerSample.Customer.Domain.AggregatesModel.BrandAggregate;
using Galaxy.Application;
using Galaxy.Cache;
using Galaxy.FluentValidation;
using Galaxy.Infrastructure;
using Galaxy.Log;
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
   public class CustomerAppService : CrudAppServiceAsync<BrandDto,int,Brand>, ICustomerAppService
    {
        private readonly ILog _log;
        private readonly ICache _cacheServ;
        private readonly IBrandRepository _brandRepository;
        private readonly IBrandPolicy _brandPolicy;
        public CustomerAppService(IBrandRepository brandRepository
            , IBrandPolicy brandPolicy
            , IObjectMapper objectMapper
            , IRepositoryAsync<Brand,int> rep
            , ICache cacheServ
            , ILog log
            , IUnitOfWorkAsync unitofWorkAsync) : base (rep,objectMapper, unitofWorkAsync)
        {
            this._brandRepository = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
            this._brandPolicy = brandPolicy ?? throw new ArgumentNullException(nameof(brandPolicy));
            this._cacheServ = cacheServ ?? throw new ArgumentNullException(nameof(cacheServ));
            this._log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public async Task<object> GetCachedBrand(string brandName) {
            this._log.Warning($"This is a test message for Serilog File!!!");
            return await  this._cacheServ.GetAsync(brandName);
        }

        public async Task<IList<BrandDto>> GetAllBrandsAsync() => await base.QueryableNoTrack().ToListAsync();
        

        public async Task AddNewBrand(BrandDto brandDto)
        {
            await AddAsync(async () =>
            {
                return Brand
                    .Create(brandDto.EMail, brandDto.BrandName, brandDto.Gsm, brandDto.SNCode);
            });
        }

        public async Task<BrandDto> GetBrandByIdAsync(int brandId) =>
            await FindAsync(brandId);

            
        public async Task AddMerchantToBrand(MerchantDto merchant)
        {
            await UpdateAsync(merchant.BrandId ,async brand => {
                brand
                   .AddMerchant(merchant.Name, merchant.Surname, merchant.BrandId, merchant.LimitId, _brandPolicy, merchant.Gsm)
                   .SyncObjectState(ObjectState.Added);
            }); 
        }

        public async Task ChangeBrandName(BrandDto brandDto)
        {
            await UpdateAsync(brandDto.Id, async brand => {
                brand
                    .ChangeBrandName(brandDto.BrandName);
            });
        }
        
        public async Task ChangeMerchantVknByBrand (MerchantDto merchant)
        {
            await UpdateAsync(merchant.BrandId, async brand => {
                brand
                    .ChangeOrAddVknToMerchant(merchant.Id, merchant.Vkn)
                    .SyncObjectState(ObjectState.Modified);
            });
        }
        
    }
}
