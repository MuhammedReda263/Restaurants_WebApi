using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domin.Entities;
using Restaurants.Domin.Interfaces;
using Restaurants.Domin.Repositories;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Requirements;
using Restaurants.Infrastructure.Authorization.Services;
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
                .AddRoles<IdentityRole>() // To make rules appear in claims
                .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>() // Custom ClaimsPrincipal
            .AddEntityFrameworkStores<RestaurantsDbContext>(); // to generate BuiltIn endpoints

            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
            services.AddScoped<IRestaurantsRepository,RestaurantsRepository>();
            services.AddScoped<IDishsRepository, DishsRepository>();

            services.AddAuthorizationBuilder()
          .AddPolicy(PolicyNames.HasNationality, builder => builder.RequireClaim(AppClaimTypes.Nationality, "German", "Polish"));
            // Who use HasNationality policy should have Nationality claim in claims's user with value German or Polish
            // we used it in Restaurant/GetById

            /////////////////////////////////////
            //For complex custom policy requirment
            services.AddAuthorizationBuilder().AddPolicy(PolicyNames.AtLeast20, builder => builder.AddRequirements(new MinimumAgeRequirement(20)));
            services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
            /////////////////////////////////////
            
            //////////////
            services.AddAuthorizationBuilder().AddPolicy(PolicyNames.CreatedAtleast2Restaurants, builder => builder.AddRequirements(new CreatedMultipleRestaurantsRequirement(2)));
            services.AddScoped<IAuthorizationHandler, CreatedMultipleRestaurantsRequirementHandler>();   
            /////////////

            services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();
            // the one can't delete and update others thinge.. no one cant create dishs of other's restaurant

        }
    }
}
