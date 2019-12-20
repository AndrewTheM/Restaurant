using AutoMapper;
using eRestaurant.DTO;
using eRestaurant.Helpers;
using eRestaurant.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eRestaurant.Controllers
{
    [ApiController]
    [Route("api/menu")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly IMapper _mapper;

        public MenuController(IMenuService menuService, IMapper mapper)
        {
            _menuService = menuService;
            _mapper = mapper;
        }

        [HttpGet()]
        public IActionResult GetMenu([FromQuery] PagingParameters pars)
        {
            var menu = _menuService.GetMenu(pars);
            var meta = _mapper.Map<PaginationHelper>(menu);
            Response.Headers.Add("pagination", JsonConvert.SerializeObject(meta));
            return Ok(menu);
        }

        [HttpGet("{id}")]
        public IActionResult GetMenuItem(int id)
        {
            var item = _menuService.GetMenuItem(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [HttpPost("create")]
        public IActionResult CreateMenuItem([FromBody] MenuItem request) // + Request
        {
            _menuService.CreateDish(request);
            return Ok();
        }
    }
}
