using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domin.Constants;
using Restaurants.Domin.Entities;
using Restaurants.Domin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization.Services
{
    public class RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> _logger,
    IUserContext _userContext) : IRestaurantAuthorizationService
    {
        public bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation)
        {
            var user = _userContext.GetCurrentUser();
           _logger.LogInformation("Authorizing user {UserEmail}, to {Operation} for restaurant {RestaurantName}",
           user!.Email,
           resourceOperation,
           restaurant.Name);

            if (resourceOperation == ResourceOperation.Read || resourceOperation == ResourceOperation.Create)
            {
                _logger.LogInformation("Create/read operation - successful authorization");
                return true;
            }

             if (resourceOperation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
            {
                _logger.LogInformation("Admin user, delete operation - successful authorization");
                return true;
            }

            if ((resourceOperation == ResourceOperation.Delete || resourceOperation == ResourceOperation.Update)
                   && user.Id == restaurant.OwnerId)
            {
                _logger.LogInformation("Restaurant owner - successful authorization");
                return true;
            }

            return false;


        }
    }
}
