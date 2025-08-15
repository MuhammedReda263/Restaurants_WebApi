using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domin.Entities;
using Restaurants.Domin.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurabtCommandHandler(IRestaurantsRepository _restaurantsRepo, ILogger<CreateRestaurabtCommandHandler> logger, IMapper mapper) : IRequestHandler<CreateRestaurantCommand, int>
    {
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new restaurant");
            logger.LogInformation("Create restaurant {@Restaurant}", request);
            var restaurant = mapper.Map<Restaurant>(request);
            return await _restaurantsRepo.CreateAsync(restaurant);
        }
    }
}
