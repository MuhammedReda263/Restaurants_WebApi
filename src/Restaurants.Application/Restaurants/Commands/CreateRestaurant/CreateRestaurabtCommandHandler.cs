using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domin.Entities;
using Restaurants.Domin.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurabtCommandHandler(IRestaurantsRepository _restaurantsRepo, ILogger<CreateRestaurabtCommandHandler> logger, IMapper mapper, IUserContext _userContext) : IRequestHandler<CreateRestaurantCommand, int>
    {
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            logger.LogInformation("{UserEmail} [{UserId}] is creating a new restaurant {@Restaurant}",
                       currentUser!.Email,
                       currentUser.Id,
                       request); 
            var restaurant = mapper.Map<Restaurant>(request);
            restaurant.OwnerId = currentUser.Id;      
            return await _restaurantsRepo.CreateAsync(restaurant);
        }
    }
}
