using Galaxy.Domain;
using Galaxy.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Domain.AggregatesModel.UserAggregate
{
    public class UserAssignedToRole : IdentityUserRoleEntity<int>
    {
        private UserAssignedToRole() : base()
        {
        }

        private UserAssignedToRole(int userId, int roleId) : this()
        {
            this.UserId = userId;
            this.RoleId = roleId;
        }

        public static UserAssignedToRole Create(int userId, int roleId)
        {
            return new UserAssignedToRole(userId, roleId);
        }
    }
}
