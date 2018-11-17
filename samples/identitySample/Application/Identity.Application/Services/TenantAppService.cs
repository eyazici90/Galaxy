using Galaxy.Infrastructure;
using Galaxy.ObjectMapping;
using Identity.Application.Abstractions.Dtos.Tenant;
using Identity.Application.Abstractions.Services;
using Identity.Domain.AggregatesModel.TenantAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Services
{
    public class TenantAppService : ITenanAppService
    {
        private readonly ITenantRepository _tenantRep;
        private readonly IObjectMapper _objectMapper;
        public TenantAppService(ITenantRepository tenantRep
            , IObjectMapper objectMapper)
        {
            _tenantRep = tenantRep ?? throw new ArgumentNullException(nameof(tenantRep));
            _objectMapper = objectMapper ?? throw new ArgumentNullException(nameof(objectMapper));
        }

        public async Task<List<TenantDto>> GetAllTenantsAsync() =>
             (this._objectMapper.MapTo<List<TenantDto>>
                    (await this._tenantRep.GetTenantsAsync()));

        public async Task<TenantDto> GetTenantByIdAsync(int id) =>
            (this._objectMapper.MapTo<TenantDto>(await this._tenantRep.GetAsync(id)));


        public void AddTenant(TenantDto tenant)
        {
            var newTenant = Tenant.Create(tenant.Name, tenant.Description);
            this._tenantRep.Add(newTenant);
        }

        public async Task DeleteTenant(int id) =>
            await this._tenantRep.Delete(id);

        public async Task UpdateTenant(TenantDto tenant)
        {
            var existingTenant = await this._tenantRep.GetAsync(tenant.Id);
            existingTenant.ChangeName(tenant.Name)
                .ChangeOrAddDesc(tenant.Description);

            existingTenant.SyncObjectState(ObjectState.Modified);
        }
    }
}
