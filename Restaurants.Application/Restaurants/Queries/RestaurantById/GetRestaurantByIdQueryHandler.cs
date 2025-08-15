using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domin.Entities;
using Restaurants.Domin.Exceptions;
using Restaurants.Domin.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Queries.RestaurantById
{
    public class GetRestaurantByIdQueryHandler(IRestaurantsRepository _restaurantsRepo, ILogger<GetRestaurantByIdQueryHandler> logger, IMapper mapper) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
    {
        public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Get Restaurant By Id : {request.id}");
            var restaurant = await _restaurantsRepo.GetByIdAsync(request.id) ?? throw new NotFoundException(nameof(Restaurant), request.id.ToString()); ;
            var resturantDto = mapper.Map<RestaurantDto>(restaurant);
            return resturantDto;
        }
    }
}
