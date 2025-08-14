using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domin.Entities;


namespace Restaurants.Application.Restaurants
{
    public interface IRestaurantsService
    {
        Task<IEnumerable<RestaurantDto>> GetAll();
        Task<RestaurantDto?> GetById(int id);
        Task<int> Create (CreateRestaurantDto dto);

    }
}
