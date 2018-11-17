using Galaxy.Infrastructure;
using Galaxy.ObjectMapping;
using Galaxy.UnitOfWork;
using Identity.Application.Abstractions.Dtos.User;
using Identity.Application.Abstractions.Services;
using Identity.Domain.AggregatesModel.UserAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _userRep;
        private readonly IUnitOfWorkAsync _unitofWork;
        private readonly IObjectMapper _objectMapper;
        public UserAppService(IUserRepository userRep
            , IObjectMapper objectMapper
            , IUnitOfWorkAsync unitofWork
           )
        {
            this._userRep = userRep ?? throw new ArgumentNullException(nameof(userRep));
            this._objectMapper = objectMapper ?? throw new ArgumentNullException(nameof(objectMapper));
            this._unitofWork = unitofWork ?? throw new ArgumentNullException(nameof(unitofWork));
        }

        public async Task<bool> AddUser(UserDto user)
        {
           return await this._userRep
                .CreateUserAsync(User.Create(user.UserName, user.TenantId), "123456.");
         
        }

        public async Task<bool> AssignRoleToUser(UserDto userDto, int roleId)
        {
            var user = await this._userRep.GetUserAggregateById(userDto.Id);
            var addedUserRole = user.AssignRole(roleId);
            addedUserRole.SyncObjectState(ObjectState.Added);
            return await Task.FromResult(true);
        }

        public async Task<List<UserDto>> GetAllUsersAsync() =>
            this._objectMapper.MapTo<List<UserDto>>(await this._userRep.GetAllUsers());



        public async Task<UserDto> GetUserByIdAsync(int userId) =>
             this._objectMapper.MapTo<UserDto>(await this._userRep.GetUserById(userId));


        public async Task<bool> UpdateUserAsync(UserDto user)
        {
            var result = await this._userRep.Update(this._objectMapper.MapTo<User>(user));
            return result;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await this._userRep.GetUserById(userId);
            return await this._userRep.Delete(user);
        }
        public async Task<UserDto> FindByUsername(string userName) =>
             this._objectMapper.MapTo<UserDto>(await this._userRep.FindByUsername(userName));


        public async Task<UserDto> ValidateCredentialsByUserName(UserCredantialsDto credentials)
        {
            var _user = (await this._userRep.FindByUsername(credentials.Username));
            if (_user == null)
                return null;

            if (await this._userRep.ValidateCredentials(_user, credentials.Password))
            {
                return this._objectMapper.MapTo<UserDto>(_user);
            }
            return null;
        }

        public async Task<bool> ValidateCredentials(UserDto user, string password)
        {
            var _user = await this._userRep.GetUserById(user.Id);
            return await this._userRep.ValidateCredentials(_user, password);
        }

        public async Task<IEnumerable<UserAssignedToRoleDto>> UserAssignedToRolesByUserId(int userId) =>
              this._objectMapper.MapTo<IEnumerable<UserAssignedToRoleDto>>(await this._userRep.UserAssignedToRolesByUserId(userId));


        public async Task<bool> AssignPermissionToUser(UserDto userDto, int permissionId)
        {
            var user = await this._userRep.GetUserAggregateById(userDto.Id);
            var permission = user.AssignPermission(permissionId);
            permission.SyncObjectState(ObjectState.Added);
            return await Task.FromResult(true);
        }
    }
}
