using Galaxy.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.AggregatesModel.TenantAggregate
{
    public interface ITenantRepository : ICustomRepository
    {
        Task<List<Tenant>> GetTenantsAsync();

        void Add(Tenant tenant);

        void Update(Tenant tenant);

        Task<bool> Delete(int tenantId);

        Task<Tenant> GetAsync(int tenantId);
    }
}
