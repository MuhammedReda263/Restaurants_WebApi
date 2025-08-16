using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishs.Dtos;
using Restaurants.Domin.Entities;
using Restaurants.Domin.Exceptions;
using Restaurants.Domin.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishs.Queries.GetDishsForRestaurant
{
    internal class GetDishsForRestaurantQueryHandler(IMapper _mapper,ILogger<GetDishsForRestaurantQueryHandler> _logger,IRestaurantsRepository _restaurantsRepository) : IRequestHandler<GetDishsForRestaurantQuery, IEnumerable<DishDto>>
    {
        public async Task<IEnumerable<DishDto>> Handle(GetDishsForRestaurantQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("retrive the dishe for the restaurant wit id : {@RestaurantId}", request.Id);
            var restaurant = await _restaurantsRepository.GetByIdAsync(request.Id);
            if (restaurant == null) 
                throw new NotFoundException(nameof(Restaurant),request.Id.ToString());   
            var result = _mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);
            return result;
        }
    }
}
