using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Application.Abstractions.Dtos.Tenant
{
    public class TenantDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Logo { get; set; }
    }
}
