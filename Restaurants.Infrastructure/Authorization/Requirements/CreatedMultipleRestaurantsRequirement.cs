using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    public class CreatedMultipleRestaurantsRequirement(int _minimumUserRequirment) : IAuthorizationRequirement
    {
        public int minimumUserRequirment = _minimumUserRequirment;
    }
}
