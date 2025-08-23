using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishs.Commands.CreateDish;
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

namespace Restaurants.Application.Dishs.Commands.DeleteDishs
{
    public class DeleteDishsForRestaurantCommandHandler(ILogger<DeleteDishsForRestaurantCommandHandler> _logger, IRestaurantsRepository _restaurantsRepository, IDishsRepository _dishsRepository, IRestaurantAuthorizationService _restaurantAuthorizationService) : IRequestHandler<DeleteDishsForRestaurantCommand>
    {
        public async Task Handle(DeleteDishsForRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogWarning("Removing all dishs for restaurant with id : {@RestaurantId}", request.restaurantId);
            var restaurant = await _restaurantsRepository.GetByIdAsync(request.restaurantId);
            if (restaurant == null)
                throw new NotFoundException(nameof(restaurant), request.restaurantId.ToString());
            if (!_restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
            {
                throw new ForbidException();
            }
            await _dishsRepository.DeleteAsync(restaurant.Dishes);

        }
    }
}
