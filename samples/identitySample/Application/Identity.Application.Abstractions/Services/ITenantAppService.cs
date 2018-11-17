using Galaxy.Application;
using Identity.Application.Abstractions.Dtos.Tenant;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Abstractions.Services
{
    public interface ITenanAppService : IApplicationService
    {
        Task<List<TenantDto>> GetAllTenantsAsync();
        Task<TenantDto> GetTenantByIdAsync(int id);
        void AddTenant(TenantDto tenant);
        Task DeleteTenant(int id);
        Task UpdateTenant(TenantDto tenant);
    }
}
