using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Seeders
{
    public interface IRestaurantSeeder
    {
        Task Seed();
    }
}
