using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishs.Commands.CreateDish;
using Restaurants.Domin.Exceptions;
using Restaurants.Domin.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishs.Commands.DeleteDishs
{
    public class DeleteDishsForRestaurantCommandHandler(ILogger<DeleteDishsForRestaurantCommandHandler> _logger, IRestaurantsRepository _restaurantsRepository, IDishsRepository _dishsRepository) : IRequestHandler<DeleteDishsForRestaurantCommand>
    {
        public async Task Handle(DeleteDishsForRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogWarning("Removing all dishs for restaurant with id : {@RestaurantId}", request.restaurantId);
            var restaursnt = await _restaurantsRepository.GetByIdAsync(request.restaurantId);
            if (restaursnt == null)
                throw new NotFoundException(nameof(restaursnt), request.restaurantId.ToString());   
            await _dishsRepository.DeleteAsync(restaursnt.Dishes);

        }
    }
}
