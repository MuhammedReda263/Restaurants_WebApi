using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    public class MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> _logger,
    IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            var currentUser = userContext.GetCurrentUser();

            _logger.LogInformation("User: {Email}, date of birth {DoB} - Handling MinimumAgeRequirement",
                currentUser!.Email,
                currentUser.DateOfBirth);
            if (currentUser.DateOfBirth == null)
            {
                _logger.LogWarning("User date of birth is null");
                context.Fail();
                return Task.CompletedTask;
            }
            if (currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
            {
                _logger.LogInformation("Authorization succeeded");
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;

        }
    }
}
