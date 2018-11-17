using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Identity.Application.Abstractions.Dtos.Permission;
using Identity.Application.Abstractions.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Host.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionAppService _permissionAppServ;
        public PermissionController(IPermissionAppService permissionAppServ)
        {
            this._permissionAppServ = permissionAppServ ?? throw new ArgumentNullException(nameof(permissionAppServ));
        }

        [Route("api/v1/Identity/Permission/{id}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPermissionByIdAsync(int id) =>
              Ok(await this._permissionAppServ.GetPermissionByIdAsync(id));

        [Route("api/v1/Identity/Permissions")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllPermissionsAsync() =>
            Ok(await this._permissionAppServ.GetAllPermissionsAsync());

        [Route("api/v1/Identity/Permission")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddPermission([FromBody] PermissionDto permission) =>
            Ok(await this._permissionAppServ.AddPermission(permission));

        [Route("api/v1/Identity/Permission")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] PermissionDto permission) =>
            Ok(await this._permissionAppServ.UpdatePermissionAsync(permission));

    }
}