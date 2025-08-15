using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Domin.Entities;
using Restaurants.Domin.Exceptions;
using Restaurants.Domin.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    internal class DeleteRestaurantCommandHandler(IRestaurantsRepository _restaurantsRepo, ILogger<CreateRestaurabtCommandHandler> _logger) : IRequestHandler<DeleteRestaurantCommand>
    {
        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Deleted Restaurant with id {request.id}");
            _logger.LogInformation("Deleting restaurant with id: {RestaurantId}", request.id);
            var resturant = await _restaurantsRepo.GetByIdAsync(request.id);
            if (resturant == null)
                throw new NotFoundException(nameof(Restaurant), request.id.ToString());
            await _restaurantsRepo.DeleteAsync(resturant);
        }

     
    }
}
