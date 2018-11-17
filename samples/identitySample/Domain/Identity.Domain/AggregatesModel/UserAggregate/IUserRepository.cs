using Galaxy.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.AggregatesModel.UserAggregate
{
    public interface IUserRepository : ICustomRepository
    {
        Task<bool> ValidateCredentials(User user, string password);
        Task<User> FindByUsername(string user);
        Task<User> GetUserById(int userId);
        Task<User> GetUserAggregateById(int userId);
        Task<IEnumerable<UserAssignedToRole>> UserAssignedToRolesByUserId(int userId);

        Task<User> GetUserAsync(ClaimsPrincipal user);
        Task<bool> CreateUserAsync(User user, string password);
        Task<bool> AddToRoleAsync(User user, string roleName);
        Task<bool> ChangePassword(User user, string currentPassword, string newPassword);

        Task<List<User>> GetAllUsers();
        IQueryable<User> GetAllUsersQueryable();
        Task<bool> Delete(User user);
        Task<bool> Update(User user);
    }
}
