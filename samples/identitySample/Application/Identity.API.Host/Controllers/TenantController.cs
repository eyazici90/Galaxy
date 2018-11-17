using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Identity.Application.Abstractions.Dtos.Tenant;
using Identity.Application.Abstractions.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Host.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TenantController : ControllerBase
    {
        private readonly ITenanAppService _tenantAppServ;
        public TenantController(ITenanAppService tenantAppServ)
        {
            _tenantAppServ = tenantAppServ ?? throw new ArgumentNullException(nameof(tenantAppServ));
        }

        [Route("api/v1/Identity/Tenant/{id}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTenantByIdAsync(int id) =>
          Ok(await this._tenantAppServ.GetTenantByIdAsync(id));

        [Route("api/v1/Identity/Tenants")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllTenantsAsync() =>
          Ok(await this._tenantAppServ.GetAllTenantsAsync());

        [Route("api/v1/Identity/Tenant")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] TenantDto tenant)
        {
            this._tenantAppServ.AddTenant(tenant);
            return Ok();
        }

        [Route("api/v1/Identity/Tenant")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] TenantDto tenant)
        {
            await this._tenantAppServ.UpdateTenant(tenant);
            return Ok();
        }

    }
}