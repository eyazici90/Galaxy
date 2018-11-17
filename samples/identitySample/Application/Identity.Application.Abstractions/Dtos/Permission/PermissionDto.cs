using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Application.Abstractions.Dtos.Permission
{
    public class PermissionDto
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EventName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string AreaName { get; set; }
        public string Url { get; set; }
    }
}
