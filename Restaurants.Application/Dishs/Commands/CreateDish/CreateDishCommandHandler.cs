using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domin.Constants;
using Restaurants.Domin.Entities;
using Restaurants.Domin.Exceptions;
using Restaurants.Domin.Interfaces;
using Restaurants.Domin.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishs.Commands.CreateDish
{
    internal class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> _logger , IRestaurantsRepository _restaurantsRepository, IDishsRepository _dishsRepository,IMapper _mapper, IRestaurantAuthorizationService _restaurantAuthorizationService) : IRequestHandler<CreateDishCommand,int>
    {
        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating new dish : {@DishRequest}", request);
            var restaurant = await _restaurantsRepository.GetByIdAsync(request.RestauratId);
            if (restaurant == null)
             throw new NotFoundException (nameof(restaurant),request.RestauratId.ToString());
            if (!_restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Create))
            {
                throw new ForbidException();
            }
            var dish = _mapper.Map<Dish>(request);
            return await _dishsRepository.CreateAsync(dish);
            
        }
    }
}
