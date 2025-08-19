using MediatR;

namespace Restaurants.Application.Users.Commands.AssignUserRole
{
    public class AssignUserRoleCommand : IRequest
    {
        public string email { get; set; } = default!;
        public string role { get; set; } = default!;
    }
}
