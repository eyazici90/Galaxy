using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Application.Abstractions.Dtos.User
{
    public class UserAssignedToPermissionDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int PermissionId { get; set; }
    }
}
