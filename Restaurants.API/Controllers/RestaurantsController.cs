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
using Restaurants.Domin.Entities;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController(IValidator<CreateRestaurantCommand> _validatorCreateRestaurantCommand, IValidator<UpdateRestaurantCommand> _validatorUpdateRestaurantCommand, IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()
        {
            var resturenats = await _mediator.Send(new GetAllRestaurantsQuery());
            return Ok(resturenats);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetById([FromRoute] int id)
        {
            var restaurant = await _mediator.Send(new GetRestaurantByIdQuery(id));
            return Ok(restaurant);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
             await _mediator.Send(new DeleteRestaurantCommand(id));           
            return NoContent();
        
        }



        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromRoute] int id,UpdateRestaurantCommand command)
        {
            var resultOfValidation = await _validatorUpdateRestaurantCommand.ValidateAsync(command);
            if (resultOfValidation.IsValid)
            {
                command.Id = id;
               await _mediator.Send(command);
               return NoContent();               
            }
            return BadRequest(resultOfValidation.Errors);
        }



    }
}