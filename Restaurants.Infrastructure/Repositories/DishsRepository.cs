

using Restaurants.Domin.Entities;
using Restaurants.Domin.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    class DishsRepository (RestaurantsDbContext _dbContext) : IDishsRepository
    {
        public async Task<int> CreateAsync(Dish dish)
        {
            _dbContext.Dishes.Add(dish);
            await _dbContext.SaveChangesAsync();
            return dish.Id;
        }

        public async Task DeleteAsync(IEnumerable<Dish> dishs)
        {
            _dbContext.Dishes.RemoveRange(dishs);
            await _dbContext.SaveChangesAsync();
        }
    }
}
