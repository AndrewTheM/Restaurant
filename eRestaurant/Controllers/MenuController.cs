using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eRestaurant;
using eRestaurant.Entities;
using eRestaurant.Services;

namespace eRestaurant.Controllers
{
    [ApiController]
    [Route("api/menu")]
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService) => _menuService = menuService;

        [HttpGet()]
        public IActionResult GetMenu()
        {
            var menu = _menuService.GetMenu();
            return Ok(menu);
        }

        [HttpGet("{id}")]
        public IActionResult GetMenuItem(int id)
        {
            var dish = _menuService.GetMenuItem(id);
            if (dish == null)
                return NotFound();
            return Ok(dish);
        }

        //// PUT: api/Catalogue/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutDish(int id, Dish dish)
        //{
        //    if (id != dish.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(dish).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DishExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Catalogue
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPost]
        //public async Task<ActionResult<Dish>> PostDish(Dish dish)
        //{
        //    _context.Dishes.Add(dish);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetDish", new { id = dish.Id }, dish);
        //}

        //// DELETE: api/Catalogue/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Dish>> DeleteDish(int id)
        //{
        //    var dish = await _context.Dishes.FindAsync(id);
        //    if (dish == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Dishes.Remove(dish);
        //    await _context.SaveChangesAsync();

        //    return dish;
        //}

        //private bool DishExists(int id)
        //{
        //    return _context.Dishes.Any(e => e.Id == id);
        //}
    }
}
