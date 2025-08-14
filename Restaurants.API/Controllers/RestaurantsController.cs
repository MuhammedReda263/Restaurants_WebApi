using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    public class RestaurantsController (IRestaurantsService _restaurantsService, IValidator<CreateRestaurantDto> _validatorreateRestaurantDto) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var resturenats = await _restaurantsService.GetAll();
            return Ok(resturenats);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var restaurant = await _restaurantsService.GetById(id);
            if (restaurant == null) 
                return NotFound();
            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRestaurantDto createRestaurantDto)
        {
            var result = await _validatorreateRestaurantDto.ValidateAsync(createRestaurantDto);
            if (result.IsValid)
            {
                var restaurantId = await _restaurantsService.Create(createRestaurantDto);
                 return CreatedAtAction(nameof(GetById), new { id = restaurantId }, null);
            }
             return BadRequest(result.Errors);
            }
        }
}
