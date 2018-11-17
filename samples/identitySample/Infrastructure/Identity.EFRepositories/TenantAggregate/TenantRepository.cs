using Galaxy.Repositories;
using Identity.Domain.AggregatesModel.TenantAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Identity.EFRepositories.TenantAggregate
{
    public class TenantRepository : ITenantRepository
    {
        private readonly IRepositoryAsync<Tenant> _tenantRep;
        public TenantRepository(IRepositoryAsync<Tenant> tenantRep)
        {
            this._tenantRep = tenantRep ?? throw new ArgumentNullException(nameof(tenantRep));
        }
        public void Add(Tenant tenant) =>
            this._tenantRep.Insert(tenant);


        public async Task<bool> Delete(int tenantId) =>
            await this._tenantRep.DeleteAsync(tenantId);


        public async Task<Tenant> GetAsync(int tenantId) =>
            await this._tenantRep.FindAsync(tenantId);


        public async Task<List<Tenant>> GetTenantsAsync() =>
            await this._tenantRep.QueryableNoTrack().ToListAsync();


        public void Update(Tenant tenant) =>
            this._tenantRep.Update(tenant);

    }
}
