using Galaxy.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.AggregatesModel.PermissionAggregate
{
    public interface IPermissionRepository : ICustomRepository
    {
        Task<List<Permission>> GetPermissionsAsync();

        void Add(Permission permission);

        void Update(Permission permission);

        Task<bool> Delete(int permissiondId);

        Task<Permission> GetAsync(int permissiondId);
    }
}
