using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domin.Entities
{
    public class User : IdentityUser
    {
        public DateOnly? DateOfBirth { get; set; }
        public string? Nationality { get; set; }
        public List<Restaurant> restaurants { get; set; } = default!;
    }
}
