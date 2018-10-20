using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Domain
{
    public static class EntityExtensions
    {
        public static bool IsNullOrDeleted(this ISoftDelete entity) =>
            entity == null || entity.IsDeleted;
        
    }
}
