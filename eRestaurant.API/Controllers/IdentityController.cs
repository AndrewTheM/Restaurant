using eRestaurant.API.Services;
using eRestaurant.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
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
            var info = await _identityService.RegisterAsync(cred.Email, cred.Password);
            return Ok(info);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Credentials cred)
        {
            var info = await _identityService.LoginAsync(cred.Email, cred.Password);
            return Ok(info);
        }
    }
}
