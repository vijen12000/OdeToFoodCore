using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantData _restaurantData;

        public RestaurantsController(IRestaurantData restaurantData)
        {
            _restaurantData = restaurantData;
        }

        // GET: api/Restaurants
        [HttpGet]
        public async Task<IEnumerable<Restaurant>> GetRestaurants()
        {
            return await _restaurantData.GetRestaurantByName();
        }

        // GET: api/Restaurants/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestaurant([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var restaurant = await _restaurantData.GetByIdAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }

        // PUT: api/Restaurants/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant([FromRoute] int id, [FromBody] Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != restaurant.Id)
            {
                return BadRequest();
            }

            _restaurantData.Update(restaurant);

            try
            {
                await _restaurantData.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await RestaurantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Restaurants
        [HttpPost]
        public async Task<IActionResult> PostRestaurant([FromBody] Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _restaurantData.Add(restaurant);
            await _restaurantData.Commit();

            return CreatedAtAction("GetRestaurant", new { id = restaurant.Id }, restaurant);
        }

        // DELETE: api/Restaurants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var restaurant = await _restaurantData.GetByIdAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            await _restaurantData.Delete(id);
            await _restaurantData.Commit();

            return Ok(restaurant);
        }

        private async Task<bool> RestaurantExists(int id)
        {
            return await _restaurantData.GetCountOfRestaurants()>0;
        }
    }
}