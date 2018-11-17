using Galaxy.Application;
using Identity.Application.Abstractions.Dtos.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Abstractions.Services
{
    public interface IUserAppService : IApplicationService
    {
        Task<bool> AssignRoleToUser(UserDto userDto, int roleId);

        Task<UserDto> FindByUsername(string userName);
        Task<bool> AssignPermissionToUser(UserDto userDto, int permissionId);
        Task<IEnumerable<UserAssignedToRoleDto>> UserAssignedToRolesByUserId(int userId);
        Task<UserDto> GetUserByIdAsync(int userId);
        Task<List<UserDto>> GetAllUsersAsync();
        Task<bool> AddUser(UserDto user);
        Task<bool> DeleteUserAsync(int userId);
        Task<bool> UpdateUserAsync(UserDto user);
        Task<bool> ValidateCredentials(UserDto user, string password);

        //[DisableValidation] Marker Attribute Usage
        Task<UserDto> ValidateCredentialsByUserName(UserCredantialsDto credentials);
    }
}
