
using Galaxy.Session;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.EFCore
{
    public class SessionBase : IAppSessionBase
    {
        public virtual Nullable<int> TenantId { get ; set ; }
        public virtual Nullable<int> UserId { get ; set ; }

        public virtual int? GetCurrenTenantId() => this.TenantId;
        public virtual int? GetCurrentUserId() => this.UserId;
    }
}
