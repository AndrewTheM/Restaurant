using eRestaurant.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace eRestaurant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        private readonly ICartService _service;

        public CartController(ICartService service) => _service = service;

        [HttpGet("{id}")]
        public IActionResult GetDishes(int id)
        {
            var dishes = _service.GetCartDishes(id);
            return Ok(dishes);
        }

        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
