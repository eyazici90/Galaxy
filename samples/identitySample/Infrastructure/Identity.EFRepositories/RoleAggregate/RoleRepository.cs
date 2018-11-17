using Galaxy.Infrastructure;
using Galaxy.Repositories;
using Identity.Domain.AggregatesModel.RoleAggregate;
using Identity.Domain.AggregatesModel.UserAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.EFRepositories.RoleAggregate
{
    public class RoleRepository : IRoleRepository
    {
        private readonly UserManager<User> _userServ;
        private readonly RoleManager<Role> _roleServ;
        private readonly IRepositoryAsync<Role> _roleRep;
        private readonly IRepositoryAsync<User> _userRep;
        public RoleRepository(UserManager<User> userServ
            , RoleManager<Role> roleServ
            , IRepositoryAsync<Role> roleRep
            , IRepositoryAsync<User> userRep)
        {
            this._userServ = userServ ?? throw new ArgumentNullException(nameof(userServ));
            this._roleServ = roleServ ?? throw new ArgumentNullException(nameof(roleServ));
            this._roleRep = roleRep ?? throw new ArgumentNullException(nameof(roleRep));
            this._userRep = userRep ?? throw new ArgumentNullException(nameof(userRep));
        }

        public Task<Role> FindRoleById(int roleId) =>
            this._roleServ.FindByIdAsync(roleId.ToString());

        public async Task<IList<string>> FindUserRolesByName(string username)
        {
            var user = await this._userServ.FindByNameAsync(username);
            return await this._userServ.GetRolesAsync(user);
        }

        public IQueryable<Role> GetAllRoles() =>
             this._roleServ.Roles;

        public async Task<Role> GetRoleAggregateById(int roleId)
        {
            var roleAggregate = await this._roleRep.Queryable()
               .SingleOrDefaultAsync(r => r.Id == roleId);
            return roleAggregate;
        }

        public async Task<IEnumerable<UserAssignedToRole>> GetUserAssignedToRoleByRoleId(int roleId) =>
             await this._userRep.Queryable()
                .Include(u => u.UserRoles)
                .SelectMany(u => u.UserRoles.Where(r => r.RoleId == roleId))
                .ToListAsync();

        public async Task<IList<string>> GetRolesByUserId(int userId)
        {
            var user = await this._userServ.FindByIdAsync(userId.ToString());
            return await this._userServ.GetRolesAsync(user);
        }

        public async Task<IList<string>> GetRolesByUser(User user) =>
            await this._userServ.GetRolesAsync(user);

        public async Task<bool> CreateAsync(Role role)
        {
            role.SyncObjectState(ObjectState.Added);
            return (await this._roleServ.CreateAsync(role)).Succeeded;
        }

        public async Task<bool> UpdateAsync(Role role)
        {
            role.SyncObjectState(ObjectState.Modified);
            return (await this._roleServ.UpdateAsync(role)).Succeeded ;
        }

        public async Task<bool> DeleteAsync(Role role)
        {
            role.SyncObjectState(ObjectState.Deleted);
            return (await this._roleServ.DeleteAsync(role)).Succeeded;
        }
    }
}
