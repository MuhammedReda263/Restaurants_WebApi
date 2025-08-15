using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Domin.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler(IRestaurantsRepository _restaurantsRepo, ILogger<CreateRestaurabtCommandHandler> _logger, IMapper _mapper) : IRequestHandler<UpdateRestaurantCommand, bool>
    {
        public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Updated Restaurant with id {request.Id}");
            _logger.LogInformation("Updating restaurant with id: {RestaurantId} with {@UpdatedRestaurant}", request.Id, request);


            var resturant = await _restaurantsRepo.GetByIdAsync(request.Id);
            if (resturant == null)
                return false;
            _mapper.Map(request, resturant);
            await _restaurantsRepo.SaveChangesAsync();
            return true;

        }
    }
}
