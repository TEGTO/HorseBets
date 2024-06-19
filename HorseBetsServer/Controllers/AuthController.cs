using Azure.Core;
using HorseBets.Api.Authentication.Models;
using HorseBets.Api.Authentication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HorseBets.Api.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration config;
        private IAuthenticationService authenticationService;

        public AuthController(IConfiguration config, IAuthenticationService authenticationService)
        {
            this.config = (IConfigurationRoot)config;
            this.authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AccessToken>> Login([FromBody] AuthenticationModelDto loginModel, CancellationToken cancellationToken)
        {
            if (loginModel == null)
                return BadRequest("Invalid client request");
            if (await CheckRegisterUserAsync(loginModel, cancellationToken))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!));
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokenOptions = new JwtSecurityToken(
                        issuer: config["JwtSettings:Issuer"],
                        audience: config["JwtSettings:Audience"],
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(10),
                        signingCredentials: signingCredentials
                );
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                var accessToken = new AccessToken(token, tokenOptions.ValidTo);
                return Ok(accessToken);
            }
            return Unauthorized();
        }

        private async Task<bool> CheckRegisterUserAsync(AuthenticationModelDto loginModel, CancellationToken cancellationToken)
        {
            return await authenticationService.CheckRegisteredUserAsync(loginModel, cancellationToken);
        }
    }
}
