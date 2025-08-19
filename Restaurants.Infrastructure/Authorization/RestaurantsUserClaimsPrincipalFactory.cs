using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurants.Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization
{
    // Custom ClaimsPrincipal For our user in the app
    public class RestaurantsUserClaimsPrincipalFactory(UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager,
    IOptions<IdentityOptions> options)
        : UserClaimsPrincipalFactory<User, IdentityRole>(userManager, roleManager, options)
    {
        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var id = await GenerateClaimsAsync(user);

            if (user.Nationality != null)
            {
                id.AddClaim(new Claim("Nationality", user.Nationality));
            }

            if (user.DateOfBirth != null)
            {
                id.AddClaim(new Claim("DateOfBirth", user.DateOfBirth.Value.ToString("yyyy-MM-dd")));
            }

            return new ClaimsPrincipal(id);

        }
    }
}
