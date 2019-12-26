using AutoMapper;
using eRestaurant.DTO;
using eRestaurant.Entities;
using eRestaurant.Helpers;
using eRestaurant.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace eRestaurant.Controllers
{
    [ApiController]
    [Route("api/menu")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService) => _menuService = menuService;

        [HttpGet]
        public IActionResult GetMenu([FromQuery] PagingParameters pars)
        {
            var menu = _menuService.GetMenu(pars);
            Response.Headers.Add("totalPages", JsonConvert.SerializeObject(menu.TotalPages));
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

        [HttpGet("dish/{id}")]
        public IActionResult GetDish(int id)
        {
            var dish = _menuService.GetDish(id);
            if (dish == null)
                return NotFound();
            return Ok(dish);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DishRequest dishDto)
        {
            int id = await _menuService.CreateDish(dishDto);
            return Ok(new DishRequest { Id = id });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DishRequest dishDto)
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
