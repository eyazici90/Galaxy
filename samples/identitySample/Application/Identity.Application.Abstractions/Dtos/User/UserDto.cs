using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Application.Abstractions.Dtos.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int TenantId { get; set; }
    }
}
