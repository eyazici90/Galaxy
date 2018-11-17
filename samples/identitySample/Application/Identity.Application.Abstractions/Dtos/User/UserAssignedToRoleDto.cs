using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Application.Abstractions.Dtos.User
{
    public class UserAssignedToRoleDto
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
