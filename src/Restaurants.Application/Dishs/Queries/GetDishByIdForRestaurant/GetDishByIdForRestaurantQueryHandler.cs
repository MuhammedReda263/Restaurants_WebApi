using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishs.Dtos;
using Restaurants.Application.Dishs.Queries.GetDishsForRestaurant;
using Restaurants.Domin.Entities;
using Restaurants.Domin.Exceptions;
using Restaurants.Domin.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishs.Queries.GetDishByIdForRestaurant
{
    public class GetDishByIdForRestaurantQueryHandler (IMapper _mapper, ILogger<GetDishByIdForRestaurantQueryHandler> _logger, IRestaurantsRepository _restaurantsRepository) : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
    {
        public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("retrive the dish with id {@DishId} for the restaurant with id : {@RestaurantId}", request.DishId,request.RestaurabtId);
            var restaurant = await _restaurantsRepository.GetByIdAsync(request.RestaurabtId);
            if (restaurant == null)
                throw new NotFoundException(nameof(Restaurant), request.RestaurabtId.ToString());
            var dish = restaurant.Dishes.FirstOrDefault(temp=>temp.Id == request.DishId);
            if (dish == null)
                throw new NotFoundException(nameof(Dish), request.DishId.ToString());
            var result = _mapper.Map<DishDto>(dish);
            return result;
        }
    }
}
