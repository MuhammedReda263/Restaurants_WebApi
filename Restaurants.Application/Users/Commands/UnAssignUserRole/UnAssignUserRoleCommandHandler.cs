using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users.Commands.AssignUserRole;
using Restaurants.Domin.Entities;
using Restaurants.Domin.Exceptions;


namespace Restaurants.Application.Users.Commands.UnAssignUserRole
{
    public class UnAssignUserRoleCommandHandler(ILogger<AssignUserRoleCommandHandler> _logger, UserManager<User> _userManager, RoleManager<IdentityRole> _roleManager) : IRequestHandler<UnAssignUserRoleCommand>
    {
        public async Task Handle(UnAssignUserRoleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("UnAssigning user role : {@Request}", request);
            var user = await _userManager.FindByEmailAsync(request.email) ?? throw new NotFoundException(nameof(User), request.email);

            var role = await _roleManager.FindByNameAsync(request.role) ?? throw new NotFoundException(nameof(IdentityRole), request.role);

            await _userManager.RemoveFromRoleAsync(user, role.Name!);
        }
    }
}
