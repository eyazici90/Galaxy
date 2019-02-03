﻿
using CustomerSample.Common.Dtos;
using Galaxy.Application;
using Galaxy.UnitOfWork;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CustomerSample.Application.Abstractions
{
    public interface ICustomerAppService : IApplicationService
    {
        [DisableUnitOfWork]
        Task<object> GetCachedBrand(string brandName);
         
        [DisableUnitOfWork]
        Task<IList<BrandDto>> GetAllBrandsAsync();
         
        [DisableUnitOfWork]
        Task<BrandDto> GetBrandByIdAsync(int brandId);
         
        [EnableUnitOfWork(isolationLevel:IsolationLevel.ReadUncommitted, timeout:20)]
        Task AddNewBrand(BrandDto brandDto);

        Task AddMerchantToBrand(MerchantDto merchant);

        Task ChangeBrandName(BrandDto brandDto);

        Task ChangeMerchantVknByBrand(MerchantDto merchant);
    }
}
