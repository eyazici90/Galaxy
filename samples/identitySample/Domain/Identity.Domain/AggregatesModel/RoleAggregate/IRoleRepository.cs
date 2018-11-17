using Galaxy.Repositories;
using Identity.Domain.AggregatesModel.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.AggregatesModel.RoleAggregate
{
    public interface IRoleRepository : ICustomRepository
    {
        Task<Role> GetRoleAggregateById(int roleId);

        Task<Role> FindRoleById(int roleId);
        IQueryable<Role> GetAllRoles();
        Task<IList<string>> GetRolesByUserId(int userId);
        Task<IList<string>> GetRolesByUser(User user);
        Task<IEnumerable<UserAssignedToRole>> GetUserAssignedToRoleByRoleId(int roleId);
        Task<bool> CreateAsync(Role role);
        Task<bool> UpdateAsync(Role role);
        Task<bool> DeleteAsync(Role role);
    }
}
