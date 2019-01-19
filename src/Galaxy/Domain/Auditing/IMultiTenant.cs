using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Domain.Auditing
{
    public interface IMultiTenant
    {
        int? TenantId { get; }

        void SyncTenantState(int? tenantId = default);
    }
}
