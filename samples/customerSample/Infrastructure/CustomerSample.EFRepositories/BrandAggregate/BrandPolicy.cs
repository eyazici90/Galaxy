
using CustomerSample.Customer.Domain.AggregatesModel.BrandAggregate;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace CustomerSample.Customer.Domain.EFRepositories.BrandAggregate
{
    public class BrandPolicy : IBrandPolicy
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IConfiguration _config;
        public BrandPolicy(IBrandRepository brandRepository
            , IConfiguration config)
        {
            _brandRepository = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public int MaxAmountofMarchantperBrand => int.Parse(this._config["MaxAmountofMarchantperBrand"]);

        public async Task<bool> CheckAssignment(int brandId)
        {
            // some logic could even use repo
            var fakeQuery = await this._brandRepository.GetAsync(brandId);
                
            //////////

            return await Task.FromResult(true);
        }
    }
}
