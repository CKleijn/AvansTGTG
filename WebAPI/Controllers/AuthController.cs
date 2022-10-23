using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _config;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        [HttpPost("api/signin")]
        public async Task<IActionResult> SignIn([FromBody] AuthCredentials authCredentials)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(authCredentials.IdentificationNumber);
                if (user != null)
                {
                    if ((await _signInManager.PasswordSignInAsync(user, authCredentials.Password, false, false)).Succeeded)
                    {
                        var securityTokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = (await _signInManager.CreateUserPrincipalAsync(user)).Identities.First(),
                            Expires = DateTime.Now.AddMinutes(int.Parse(_config["BearerTokens:ExpiryMinutes"])),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["BearerTokens:Key"])), SecurityAlgorithms.HmacSha256Signature)
                        };

                        var handler = new JwtSecurityTokenHandler();
                        var securityToken = new JwtSecurityTokenHandler().CreateToken(securityTokenDescriptor);

                        return Ok(new { Succes = true, Token = handler.WriteToken(securityToken) });
                    }

                    ModelState.AddModelError("WrongPassword", "Wachtwoord is onjuist!");
                }
                else
                {
                    ModelState.AddModelError("NoUser", "Er bestaat geen gebruiker met deze gegevens!");
                }
            }

            return BadRequest(ModelState);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("api/signout")]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return Ok($"Uitgelogd als {User.Identity?.Name}");
        }
    }
}
