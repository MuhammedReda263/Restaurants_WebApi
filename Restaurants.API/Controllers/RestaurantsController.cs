using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurant;
using Restaurants.Application.Restaurants.Queries.RestaurantById;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController(IValidator<CreateRestaurantCommand> _validatorCreateRestaurantCommand, IValidator<UpdateRestaurantCommand> _validatorUpdateRestaurantCommand, IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllRestaurantsQuery query)
        {
            var resturenats = await _mediator.Send(query);
            return Ok(resturenats);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var restaurant = await _mediator.Send(new GetRestaurantByIdQuery(id));
            if (restaurant == null)
                return NotFound();
            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRestaurantCommand createRestaurantCommand)
        {
            var result = await _validatorCreateRestaurantCommand.ValidateAsync(createRestaurantCommand);
            if (result.IsValid)
            {
                var restaurantId = await _mediator.Send(createRestaurantCommand);
                return CreatedAtAction(nameof(GetById), new { id = restaurantId }, null);
            }
            return BadRequest(result.Errors);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteRestaurantCommand(id));
            if(result)
            return NoContent();
            return NotFound();
        }



        [HttpPatch("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id,UpdateRestaurantCommand command)
        {
            var resultOfValidation = await _validatorUpdateRestaurantCommand.ValidateAsync(command);
            if (resultOfValidation.IsValid)
            {
                command.Id = id;
                var result = await _mediator.Send(command);
                if (result)
                    return NoContent();
                return NotFound();
            }
            return BadRequest(resultOfValidation.Errors);
        }



    }
}