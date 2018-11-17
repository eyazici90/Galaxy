using Galaxy.ObjectMapping;
using Galaxy.UnitOfWork;
using Identity.Application.Abstractions.Dtos.Role;
using Identity.Application.Abstractions.Dtos.User;
using Identity.Application.Abstractions.Services;
using Identity.Domain.AggregatesModel.RoleAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Services
{
    public class RoleAppService : IRoleAppService
    {

        private readonly IRoleRepository _roleRep;
        private readonly IUnitOfWorkAsync _unitofWork;
        private readonly IObjectMapper _objectMapper;
        public RoleAppService(IRoleRepository roleRep
            , IObjectMapper objectMapper
            , IUnitOfWorkAsync unitofWork)
        {
            this._roleRep = roleRep ?? throw new ArgumentNullException(nameof(roleRep));
            this._objectMapper = objectMapper ?? throw new ArgumentNullException(nameof(objectMapper));
            this._unitofWork = unitofWork ?? throw new ArgumentNullException(nameof(unitofWork));
        }

        public async Task<IEnumerable<UserAssignedToRoleDto>> GetUserAssignedToRoleByRoleId(int roleId)
        {
            return this._objectMapper.MapTo<IEnumerable<UserAssignedToRoleDto>>
                        (await this._roleRep.GetUserAssignedToRoleByRoleId(roleId));
        }


        public async Task<bool> AddRole(RoleDto roleDto)
        {
            var role = Role.Create(roleDto.Name);
            return await this._roleRep.CreateAsync(role);
        }

        public async Task<RoleDto> GetRoleByIdAsync(int roleId) =>
            this._objectMapper.MapTo<RoleDto>(
                    (await this._roleRep.FindRoleById(roleId))
                );

        public async Task<IList<RoleDto>> GetAllRolesAsync() =>
            this._objectMapper.MapTo<IList<RoleDto>>(
                    (await this._roleRep.GetAllRoles().ToListAsync())
                );

        public async Task<bool> UpdateRole(RoleDto role)
        {
            var existingRole = await this._roleRep.FindRoleById(role.Id);
            existingRole
                .ChangeName(role.Name);
            return await this._roleRep.UpdateAsync(existingRole);
        }
    }
}
