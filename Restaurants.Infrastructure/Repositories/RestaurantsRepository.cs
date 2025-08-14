using Microsoft.EntityFrameworkCore;
using Restaurants.Domin.Entities;
using Restaurants.Domin.Repositories;
using Restaurants.Infrastructure.Persistence;


namespace Restaurants.Infrastructure.Repositories
{
    internal class RestaurantsRepository (RestaurantsDbContext _dbContext) : IRestaurantsRepository
    {
        public async Task<int> Create(Restaurant restaurant)
        {
            _dbContext.Add(restaurant);
            await _dbContext.SaveChangesAsync();
            return restaurant.Id;
        }

        public async Task<IEnumerable<Restaurant>> GetAll()
        {
            return await _dbContext.Restaurants.Include(temp=>temp.Dishes).ToListAsync();
        }

        public async Task<Restaurant?> GetById(int id)
        {
           return await _dbContext.Restaurants.Include(temp=>temp.Dishes).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
