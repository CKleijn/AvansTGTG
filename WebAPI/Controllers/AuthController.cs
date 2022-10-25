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
        public async Task<IActionResult> SignUserIn([FromBody] AuthCredentials authCredentials)
        {
            try
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

                        return Ok(new
                        {
                            StatusCode = 200,
                            Message = "Je bent succesvol ingelogd!",
                            Token = handler.WriteToken(securityToken)
                        });
                    }

                    throw new Exception("Wachtwoord is onjuist!");
                }
                else
                {
                    throw new Exception("Er bestaat geen gebruiker met deze gegevens!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = e.Message
                });
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("api/signout")]
        public async Task<IActionResult> SignUserOut()
        {
            await _signInManager.SignOutAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Je bent succesvol uitgelogd!"
            });
        }
    }
}
