using Galaxy.Application;
using Galaxy.UnitOfWork;
using Identity.Application.Abstractions.Dtos.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Abstractions.Services
{
    public interface IUserAppService : IApplicationService
    {
        [EnableUnitOfWork]
        Task<bool> AssignRoleToUser(UserDto userDto, int roleId);

        Task<UserDto> FindByUsername(string userName);

        [EnableUnitOfWork]
        Task<bool> AssignPermissionToUser(UserDto userDto, int permissionId);

        Task<IEnumerable<UserAssignedToRoleDto>> UserAssignedToRolesByUserId(int userId);

        Task<UserDto> GetUserByIdAsync(int userId);

        Task<List<UserDto>> GetAllUsersAsync();

        [EnableUnitOfWork]
        Task<bool> AddUser(UserDto user);

        [EnableUnitOfWork]
        Task<bool> DeleteUserAsync(int userId);

        [EnableUnitOfWork]
        Task<bool> UpdateUserAsync(UserDto user);

        Task<bool> ValidateCredentials(UserDto user, string password);
         
        Task<UserDto> ValidateCredentialsByUserName(UserCredantialsDto credentials);
    }
}
