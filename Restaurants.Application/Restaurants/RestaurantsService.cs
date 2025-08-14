using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domin.Entities;
using Restaurants.Domin.Repositories;

namespace Restaurants.Application.Restaurants
{
    public class RestaurantsService (IRestaurantsRepository _restaurantsRepo , ILogger <RestaurantsService> logger,IMapper mapper) :IRestaurantsService
    {
     
        public async Task<IEnumerable<RestaurantDto>> GetAll()
        {
            logger.LogInformation("Getting all restaurants");
           var restaurants =  await _restaurantsRepo.GetAll();
            var restaurantsDto = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
            return restaurantsDto!;


        }

        public async Task<RestaurantDto?> GetById(int id)
        {
               var restaurant = await _restaurantsRepo.GetById(id);
               var resturantDto = mapper.Map<RestaurantDto>(restaurant);
               return resturantDto;
        }

        public async Task<int> Create(CreateRestaurantDto dto)
        {
           var restaurant = mapper.Map<Restaurant>(dto);
           return await _restaurantsRepo.Create(restaurant);
        }
    }
}
