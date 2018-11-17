using Galaxy.Repositories;
using Identity.Domain.AggregatesModel.PermissionAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Identity.EFRepositories.PermissionAggregate
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly IRepositoryAsync<Permission> _permissionRep;
        public PermissionRepository(IRepositoryAsync<Permission> permissionRep)
        {
            this._permissionRep = permissionRep ?? throw new ArgumentNullException(nameof(permissionRep));
        }

        public void Add(Permission permission) =>
            this._permissionRep.Insert(permission);


        public async Task<bool> Delete(int permissiondId) =>
            await this._permissionRep.DeleteAsync(permissiondId);


        public async Task<Permission> GetAsync(int permissiondId) =>
            await this._permissionRep.FindAsync(permissiondId);



        public async Task<List<Permission>> GetPermissionsAsync() =>
            await this._permissionRep.QueryableNoTrack()
                .ToListAsync();


        public void Update(Permission permission) =>
            this._permissionRep.Update(permission);

    }
}
