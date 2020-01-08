using eRestaurant.API.DTO;
using eRestaurant.API.Services;
using eRestaurant.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace eRestaurant.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService) => _identityService = identityService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Credentials cred)
        {
            if (!ModelState.IsValid)
                return BadRequest(new AuthResponse { Errors = ModelState.Values.SelectMany(v => v.Errors.Select(er => er.ErrorMessage)) });

            var authResponse = await _identityService.RegisterAsync(cred.Email, cred.Password);
            if (!authResponse.Success)
                return BadRequest(authResponse);
            return Ok(authResponse);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Credentials cred)
        {
            var authResponse = await _identityService.LoginAsync(cred.Email, cred.Password);
            if (!authResponse.Success)
                return BadRequest(authResponse);
            return Ok(authResponse);
        }
    }
}
