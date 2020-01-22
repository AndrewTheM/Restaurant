using eRestaurant.API.DTO;
using eRestaurant.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using SharedDto = eRestaurant.Shared.DTO;

namespace eRestaurant.API.Controllers
{
    [ApiController]
    [Route("api/menu")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService) => _menuService = menuService;

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetMenu([FromQuery] PagingParameters paging, [FromQuery] FilteringParameters filter)
        {
            var menu = _menuService.GetMenu(paging, filter);
            Response.Headers.Add("totalPages", JsonConvert.SerializeObject(menu.TotalPages));
            return Ok(menu);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetMenuItem(int id)
        {
            var item = _menuService.GetMenuItem(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [HttpGet("dish/{id}")]
        [AllowAnonymous]
        public IActionResult GetDish(int id)
        {
            var dish = _menuService.GetDish(id);
            if (dish == null)
                return NotFound();
            return Ok(dish);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SharedDto.Dish dishDto)
        {
            int id = await _menuService.CreateDish(dishDto);
            return Ok(new SharedDto.Dish { Id = id });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SharedDto.Dish dishDto)
        {
            await _menuService.UpdateDish(dishDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _menuService.DeleteDish(id);
            return RedirectToAction("GetMenu");
        }
    }
}
