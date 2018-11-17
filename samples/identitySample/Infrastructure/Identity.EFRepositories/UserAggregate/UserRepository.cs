using Galaxy.Infrastructure;
using Galaxy.Repositories;
using Identity.Domain.AggregatesModel.UserAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.EFRepositories.UserAggregate
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IRepositoryAsync<User> _userRep;
        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager
            , IRepositoryAsync<User> userRep)
        {
            this._userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this._signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this._userRep = userRep ?? throw new ArgumentNullException(nameof(userRep));
        }

        public Task<List<User>> GetAllUsers() =>
            this._userManager.Users.ToListAsync();


        public Task<User> GetUserById(int userId) =>
            this._userManager.FindByIdAsync(userId.ToString());


        public IQueryable<User> GetAllUsersQueryable() =>
            this._userManager.Users;


        public async Task<IEnumerable<UserAssignedToRole>> UserAssignedToRolesByUserId(int userId) =>
            await this._userRep.Queryable()
                .Include(u => u.UserRoles)
                .SelectMany(u => u.UserRoles.Where(r => r.UserId == userId))
                .ToListAsync();

        public async Task<User> FindByUsername(string user) =>
            await this._userRep.QueryableWithNoFilter()
                .SingleOrDefaultAsync(u => u.UserName == user.Trim());
      
        public async Task<bool> ValidateCredentials(User user, string password) =>
            await _userManager.CheckPasswordAsync(user, password);


        public async Task<bool> CreateUserAsync(User user, string password)
        {
            user.SyncObjectState(ObjectState.Added);
            return (await _userManager.CreateAsync(user, password)).Succeeded ;
        }

        public async Task<bool> AddToRoleAsync(User user, string roleName) =>
            (await this._userManager.AddToRoleAsync(user, roleName)).Succeeded;

        public async Task<bool> ChangePassword(User user, string currentPassword, string newPassword) =>
            (await this._userManager.ChangePasswordAsync(user, currentPassword, newPassword)).Succeeded ;

        public Task SignIn(User user) =>
            this._signInManager.SignInAsync(user, true);


        public Task SignOut() =>
            this._signInManager.SignOutAsync();


        public Task<User> GetUserAsync(ClaimsPrincipal user) =>
            _userManager.GetUserAsync(user);


        public async Task<bool> Delete(User user)
        {
            user.SyncObjectState(ObjectState.Deleted);
            return (await this._userManager.DeleteAsync(user)).Succeeded;
        }
        public async Task<bool> Update(User user)
        {
            this._userRep.Update(user);
            return await Task.FromResult(true);
        }

        public async Task<User> GetUserAggregateById(int userId) =>
            await this._userRep.Queryable()
                .Include(r => r.UserRoles)
                .SingleOrDefaultAsync(u => u.Id == userId);

    }
}
