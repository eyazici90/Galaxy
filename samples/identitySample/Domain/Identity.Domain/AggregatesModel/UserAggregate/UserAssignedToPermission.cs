using Galaxy.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Domain.AggregatesModel.UserAggregate
{
    public class UserAssignedToPermission : Entity
    {
        public int UserId { get; private set; }

        public int PermissionId { get; private set; }

        private UserAssignedToPermission() : base()
        {
        }

        private UserAssignedToPermission(int userId, int permissionId) : this()
        {
            this.UserId = userId;
            this.PermissionId = permissionId;
        }

        public static UserAssignedToPermission Create(int userId, int permissionId)
        {
            return new UserAssignedToPermission(userId, permissionId);
        }

    }
}
