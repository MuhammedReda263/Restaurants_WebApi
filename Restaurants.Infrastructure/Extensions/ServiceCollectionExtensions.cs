using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domin.Entities;
using Restaurants.Domin.Repositories;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;


namespace Restaurants.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            services.AddDbContext<RestaurantsDbContext>(options =>
                options.UseSqlServer(connectionString).EnableSensitiveDataLogging());
            //EnableSensitiveDataLogging => It's for allowed display senstive data in logging files in queries of EF like Id

            services.AddIdentityApiEndpoints<User>()
            .AddEntityFrameworkStores<RestaurantsDbContext>(); // to generate builtin endpoints

            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
            services.AddScoped<IRestaurantsRepository,RestaurantsRepository>();
            services.AddScoped<IDishsRepository, DishsRepository>();
        }
    }
}
