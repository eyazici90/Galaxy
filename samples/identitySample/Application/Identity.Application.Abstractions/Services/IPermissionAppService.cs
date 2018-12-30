using Galaxy.Application;
using Galaxy.UnitOfWork;
using Identity.Application.Abstractions.Dtos.Permission;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Abstractions.Services
{
    public interface IPermissionAppService : IApplicationService
    {
        [EnableUnitOfWork]
        Task<bool> AddPermission(PermissionDto permissionDto);

        Task<PermissionDto> GetPermissionByIdAsync(int permissionId);

        Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync();

        [EnableUnitOfWork]
        Task<bool> DeletePermissionAsync(int permissionId);

        [EnableUnitOfWork]
        Task<bool> UpdatePermissionAsync(PermissionDto permission);
    }
}
