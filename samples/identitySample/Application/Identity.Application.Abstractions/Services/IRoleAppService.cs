using Galaxy.Application;
using Galaxy.UnitOfWork;
using Identity.Application.Abstractions.Dtos.Role;
using Identity.Application.Abstractions.Dtos.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Abstractions.Services
{
    public interface IRoleAppService : IApplicationService
    {
        [EnableUnitOfWork]
        Task<bool> AddRole(RoleDto roleDto);

        Task<IEnumerable<UserAssignedToRoleDto>> GetUserAssignedToRoleByRoleId(int roleId);

        Task<RoleDto> GetRoleByIdAsync(int roleId);

        Task<IList<RoleDto>> GetAllRolesAsync();

        [EnableUnitOfWork]
        Task<bool> UpdateRole(RoleDto role);
    }
}
