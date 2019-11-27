using eRestaurant.DTO;
using eRestaurant.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRestaurant.Controllers
{
    [Route("/api/identity")]
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();

            bool authResponse = await _identityService.RegisterAsync(request.Email, request.Password);

            if (!authResponse) return BadRequest();

            return Ok();
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            bool authResponse = await _identityService.LoginAsync(request.Email, request.Password);

            if (!authResponse) return BadRequest();

            return Ok();
        }
    }
}
