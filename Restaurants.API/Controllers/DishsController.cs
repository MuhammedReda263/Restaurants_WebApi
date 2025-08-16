using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishs.Commands.CreateDish;
using Restaurants.Application.Dishs.Commands.DeleteDishs;
using Restaurants.Application.Dishs.Dtos;
using Restaurants.Application.Dishs.Queries.GetDishByIdForRestaurant;
using Restaurants.Application.Dishs.Queries.GetDishsForRestaurant;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;

namespace Restaurants.API.Controllers
{
    [Route("api/restaurants/{restaurantId}/[controller]")]
    [ApiController]
    public class DishsController(IMediator _mediator, IValidator<CreateDishCommand> _validatorDishCommand) : ControllerBase
    { 
        [HttpPost]
        public async Task<IActionResult> Create([FromRoute] int restaurantId , CreateDishCommand createDishCommand )
        {
            var result = await _validatorDishCommand.ValidateAsync(createDishCommand);
            if (result.IsValid)
            {
                createDishCommand.RestauratId = restaurantId;
               var dishId =  await _mediator.Send(createDishCommand);
                return CreatedAtAction(nameof(GetByIdForRestaurant), new {restaurantId, dishId},null);
            }
            return BadRequest(result.Errors);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetAllForRestaurant ([FromRoute]int restaurantId)
        {
            var dishs =  await _mediator.Send(new GetDishsForRestaurantQuery(restaurantId));
            return Ok (dishs);
        }

         [HttpGet("{DishId}")]
        public async Task<ActionResult<DishDto>> GetByIdForRestaurant ([FromRoute]int restaurantId, [FromRoute] int DishId)
        {
            var dish =  await _mediator.Send(new GetDishByIdForRestaurantQuery(restaurantId,DishId));
            return Ok (dish);
        }


        [HttpDelete]
        public async Task<ActionResult<DishDto>> DeleteDishsForRestaurant([FromRoute] int restaurantId)
        {
             await _mediator.Send(new DeleteDishsForRestaurantCommand(restaurantId));
            return NoContent();
        }

    }

}
