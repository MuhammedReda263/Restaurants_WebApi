using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Users
{
    public record CurrentUser(string id, string email, IEnumerable<string> roles)
    { 
        public bool IsInRole (string role) => role.Contains(role);
    }
}
