using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Session
{
   public interface IAppSessionContext
    {
        Nullable<int> TenantId { get; set; }
        Nullable<int> UserId { get; set; }

        Nullable<int> GetCurrenTenantId();
    }
}
