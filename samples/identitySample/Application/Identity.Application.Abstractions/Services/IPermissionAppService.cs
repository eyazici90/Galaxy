using Galaxy.Application;
using Identity.Application.Abstractions.Dtos.Permission;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Abstractions.Services
{
    public interface IPermissionAppService : IApplicationService
    {
        Task<bool> AddPermission(PermissionDto permissionDto);
        Task<PermissionDto> GetPermissionByIdAsync(int permissionId);
        Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync();
        Task<bool> DeletePermissionAsync(int permissionId);
        Task<bool> UpdatePermissionAsync(PermissionDto permission);
    }
}
