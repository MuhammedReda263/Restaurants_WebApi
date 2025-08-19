using Microsoft.EntityFrameworkCore;
using Restaurants.Domin.Entities;
using Restaurants.Domin.Repositories;
using Restaurants.Infrastructure.Persistence;


namespace Restaurants.Infrastructure.Repositories
{
    internal class RestaurantsRepository(RestaurantsDbContext _dbContext) : IRestaurantsRepository
    {
        public async Task<int> CreateAsync(Restaurant restaurant)
        {
            _dbContext.Add(restaurant);
            await _dbContext.SaveChangesAsync();
            return restaurant.Id;
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            return await _dbContext.Restaurants.Include(temp => temp.Dishes).ToListAsync();
        }

        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            return await _dbContext.Restaurants.Include(temp => temp.Dishes).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeleteAsync(Restaurant restaurant)
        {
            _dbContext.Remove(restaurant);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<(IEnumerable<Restaurant>,int)> GetAllMatchingAsync(string? searchPhrase,int PageNumber, int PageSize)
        {
            var searchPhraseLower = searchPhrase?.ToLower();
            var baseQuery = _dbContext.Restaurants.Where(r => searchPhraseLower == null || (r.Name.ToLower().Contains(searchPhraseLower) || r.Description.ToLower().Contains(searchPhraseLower)));
            var total = await baseQuery.CountAsync();
            var restaurants =  await baseQuery.Skip(PageSize * (PageNumber - 1)).Take(PageSize).ToListAsync();
            return (restaurants, total);
        }
    }
}
