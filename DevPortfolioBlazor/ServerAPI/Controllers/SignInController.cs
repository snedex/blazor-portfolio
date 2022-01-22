using Core.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration config;

        public SignInController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IConfiguration config)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] UserViewModel user)
        {
            string username = user.EmailAddress;

            var result = await signInManager.PasswordSignInAsync(user.EmailAddress, user.Password, false, false);

            if(result.Succeeded)
            {
                var identityUser = await userManager.FindByNameAsync(user.EmailAddress);
                var token = await GenerateJWTToken(identityUser);

                return Ok(token);
            }else
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Does the JWT token generation 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [NonAction]
        [ApiExplorerSettings(IgnoreApi =true)]
        private async Task<string> GenerateJWTToken(IdentityUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            var roles = await userManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(r => new Claim(ClaimsIdentity.DefaultRoleClaimType, r)));

            JwtSecurityToken securityToken = new
            (
                config["Jwt:Issuer"],
                config["Jwt:Issuer"],
                claims,
                null,
                expires: DateTime.UtcNow.AddDays(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
