using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Identity.Application.Abstractions.Dtos.Role;
using Identity.Application.Abstractions.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Host.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RoleController : ControllerBase
    {
        private readonly IRoleAppService _roleAppServ;
        public RoleController(IRoleAppService roleAppServ)
        {
            _roleAppServ = roleAppServ ?? throw new ArgumentNullException(nameof(roleAppServ));
        }

        [Route("api/v1/Identity/Role/{id}/Users")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUserAssignedToRoleByRoleId(int id) =>
          Ok(await this._roleAppServ.GetUserAssignedToRoleByRoleId(id));

        [Route("api/v1/Identity/Role")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] RoleDto role) =>
           Ok(await this._roleAppServ.AddRole(role));

        [Route("api/v1/Identity/Role/{id}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetRoleByIdAsync([FromRoute]int id) =>
           Ok(await this._roleAppServ.GetRoleByIdAsync(id));

        [Route("api/v1/Identity/Roles")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllRolesAsync() =>
          Ok(await this._roleAppServ.GetAllRolesAsync());

        [Route("api/v1/Identity/Role")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] RoleDto role) =>
         Ok(await this._roleAppServ.UpdateRole(role));

    }
}