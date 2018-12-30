using Galaxy.Infrastructure;
using Galaxy.ObjectMapping;
using Galaxy.UnitOfWork;
using Identity.Application.Abstractions.Dtos.Permission;
using Identity.Application.Abstractions.Services;
using Identity.Domain.AggregatesModel.PermissionAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Services
{
    public class PermissionAppService : IPermissionAppService
    {
        private readonly IPermissionRepository _permissionRep;
        private readonly IObjectMapper _objectMapper;
        public PermissionAppService(IPermissionRepository permissionRep
            , IObjectMapper objectMapper)
        {
            _permissionRep = permissionRep ?? throw new ArgumentNullException(nameof(permissionRep));
            _objectMapper = objectMapper ?? throw new ArgumentNullException(nameof(objectMapper));
        }
        public async Task<bool> AddPermission(PermissionDto permissionDto)
        {
            var permission = Permission.Create(permissionDto.Name);
            this._permissionRep.Add(permission);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeletePermissionAsync(int permissionId)
        {
            return await this._permissionRep.Delete(permissionId);
        }

        public async Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync()
        {
            return this._objectMapper.MapTo<IEnumerable<PermissionDto>>
                (await this._permissionRep.GetPermissionsAsync());
        }

        public async Task<PermissionDto> GetPermissionByIdAsync(int permissionId)
        {
            return this._objectMapper.MapTo<PermissionDto>(await this._permissionRep.GetAsync(permissionId));
        }

        public async Task<bool> UpdatePermissionAsync(PermissionDto permissionDto)
        {
            var permission = await this._permissionRep.GetAsync(permissionDto.Id);
            permission
                .ChangeName(permissionDto.Name)
                .ChangeOrSetUrl(permissionDto.Url);
            return await Task.FromResult(true);
        }
    }
}
