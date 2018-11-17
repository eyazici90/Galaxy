using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Identity.Application.Abstractions.Dtos.User;
using Identity.Application.Abstractions.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Host.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IUserAppService _userAppServ;
        public UserController(IUserAppService userAppServ)
        {
            _userAppServ = userAppServ ?? throw new ArgumentNullException(nameof(userAppServ));
        }

    
        [Route("api/v1/Identity/User/{id}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUserById(int id) =>
               Ok(await this._userAppServ.GetUserByIdAsync(id));

        [Route("api/v1/Identity/User/{id}/Roles")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UserAssignedToRolesByUserId(int id) =>
               Ok(await this._userAppServ.UserAssignedToRolesByUserId(id));

        [Route("api/v1/Identity/Users")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllUsers() =>
               Ok(await this._userAppServ.GetAllUsersAsync());

        [Route("api/v1/Identity/User")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] UserDto user) =>
               Ok(await this._userAppServ.AddUser(user));

        [Route("api/v1/Identity/User")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UserDto user) =>
              Ok(await this._userAppServ.UpdateUserAsync(user));

        [Route("api/v1/Identity/User/{id}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromRoute]int id) =>
              Ok(await this._userAppServ.DeleteUserAsync(id));

        [Route("api/v1/Identity/User/ValidateCredentials")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> ValidateCredentials([FromBody] UserDto user) =>
             Ok(await this._userAppServ.ValidateCredentials(user, "123456."));

        [Route("api/v1/Identity/User/AssignToRole")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AssignRoleToUser([FromBody] UserDto user, [FromQuery]int roleId) =>
             Ok(await this._userAppServ.AssignRoleToUser(user, roleId));

        [Route("api/v1/Identity/User/AssignToPermission")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AssignToPermission([FromBody] UserDto user, [FromQuery]int permissionId) =>
             Ok(await this._userAppServ.AssignPermissionToUser(user, permissionId));

    }
}