using Restaurants.Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domin.Repositories
{
    public interface IDishsRepository
    {
         Task<int> CreateAsync(Dish dish);
         Task DeleteAsync (IEnumerable<Dish> dishs);
    }
}
