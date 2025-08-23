using MediatR;


namespace Restaurants.Application.Users.Commands.UnAssignUserRole
{
    public class UnAssignUserRoleCommand : IRequest
    {
        public string email { get; set; } = default!;
        public string role { get; set; } = default!;
    }
}
