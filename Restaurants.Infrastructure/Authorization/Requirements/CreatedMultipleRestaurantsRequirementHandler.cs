using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.Users;
using Restaurants.Domin.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    public class CreatedMultipleRestaurantsRequirementHandler(IRestaurantsRepository _restaurantsRepository,
        IUserContext _userContext) : AuthorizationHandler<CreatedMultipleRestaurantsRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipleRestaurantsRequirement requirement)
        {
           var currentUser = _userContext.GetCurrentUser();
            var restaurants = await _restaurantsRepository.GetAllAsync();
            var count = restaurants.Count(temp=>temp.OwnerId==currentUser!.Id);
            if (count < requirement.minimumUserRequirment)
            {
               context.Succeed(requirement);
            }

            context.Fail();
        }
    }
}
