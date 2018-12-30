using Galaxy.Application;
using Galaxy.UnitOfWork;
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

        [EnableUnitOfWork]
        void AddTenant(TenantDto tenant);

        [EnableUnitOfWork]
        Task DeleteTenant(int id);

        [EnableUnitOfWork]
        Task UpdateTenant(TenantDto tenant);
    }
}
