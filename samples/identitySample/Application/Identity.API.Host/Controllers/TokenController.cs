using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Galaxy.Auditing;
using Identity.API.Host.Extensions;
using Identity.Application.Abstractions.Dtos.User;
using Identity.Application.Abstractions.RequestObjects;
using Identity.Application.Abstractions.ResponseObjects;
using Identity.Application.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Identity.API.Host.Controllers
{
    [Route("/")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IUserAppService _userAppService;
        public TokenController(IUserAppService userAppService)
        {
            _userAppService = userAppService ?? throw new ArgumentNullException(nameof(userAppService));
        }

        [Route("/api/v1/token")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody] GetTokenRequest request)
        {
            var claimList = (await IsValid(new UserCredantialsDto { Username = request.Username, Password = request.Password }));
            if (claimList.Any())
            {
                return Ok(Generate(request.Username, claimList.ToList()));
            }
            return BadRequest(new GetTokenResponse
            {
                Token = $"Invalid username or password for {request.Username}"
            });
        }

        [NonAction]
        private async Task<IList<Claim>> IsValid(UserCredantialsDto credentials)
        {
            IList<Claim> _claimList = new List<Claim>();
            var validUser = await _userAppService.ValidateCredentialsByUserName(credentials);
            if (validUser != null)
            {
                var user = await this._userAppService.FindByUsername(credentials.Username);
                _claimList.Add(new Claim(ClaimTypes.UserData, user.Id.ToString()));
                _claimList.Add(new Claim(nameof(IFullyAudit.TenantId), user.TenantId.ToString()));
            }
            return _claimList; ;
        }


        [NonAction]
        private GetTokenResponse Generate(string username, List<Claim> claimList)
        {
            var dtExpired = new DateTimeOffset(DateTime.Now.AddMinutes(180));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, dtExpired.ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, $"IdentityIssuer"),
                new Claim(JwtRegisteredClaimNames.Aud, $"IdentityAudience")
            };

            if (claimList != null || claimList.Any())
                claims.AddRange(claimList);


            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(SecurityKeyExtension.GetSigningKey("IdentityAPIseckey2017!.#")
                                            , SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            return new GetTokenResponse
            {
                ExpiredDate = dtExpired,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

    }
}