using Galaxy.Domain;
using Galaxy.Identity;
using Identity.Domain.Events;
using Identity.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Identity.Domain.AggregatesModel.UserAggregate
{
    public sealed class User : FullyAuditIdentityUserEntity<int>, IAggregateRoot
    {
        private List<UserAssignedToRole> _userRoles;

        public IEnumerable<UserAssignedToRole> UserRoles => _userRoles.AsEnumerable();

        private List<UserAssignedToPermission> _userPermissions;

        public IEnumerable<UserAssignedToPermission> UserPermissions => _userPermissions.AsEnumerable();

        private User() : base()
        {
            _userRoles = new List<UserAssignedToRole>();
            _userPermissions = new List<UserAssignedToPermission>();
        }

        private User(string userName, int tenantId) : this()
        {
            this.UserName = !string.IsNullOrWhiteSpace(userName) ? userName
                                                                 : throw new ArgumentNullException(nameof(userName));
            if (tenantId < 0)
                throw new IdentityDomainException($"Invalid Tenant Id : {tenantId}");

            TenantId = tenantId;
        }

        public static User Create(string userName, int tenantId)
        {
            return new User(userName, tenantId);
        }

        public UserAssignedToRole AssignRole(int roleId)
        {
            if (roleId < 0)
                throw new IdentityDomainException($"Invalid RoleId : {roleId}");
            var userRole = UserAssignedToRole.Create(this.Id, roleId);
            this._userRoles.Add(userRole);
            AddDomainEvent(new PermissionAssignedToUserDomainEvent(this));
            return userRole;
        }

        public UserAssignedToPermission AssignPermission(int permissionId)
        {
            if (permissionId < 0)
                throw new IdentityDomainException($"Invalid PermissionId : {permissionId}");
            var userPermission = UserAssignedToPermission.Create(this.Id, permissionId);
            this._userPermissions.Add(userPermission);
            AddDomainEvent(new PermissionAssignedToUserDomainEvent(this));
            return userPermission;
        }

    }
}
