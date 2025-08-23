using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domin.Entities;
using Restaurants.Domin.Exceptions;


namespace Restaurants.Application.Users.Commands.AssignUserRole
{
    public class AssignUserRoleCommandHandler(ILogger<AssignUserRoleCommandHandler> _logger, UserManager<User> _userManager, RoleManager<IdentityRole> _roleManager) : IRequestHandler<AssignUserRoleCommand>
    {
        public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Assigning user role : {@Request}", request);
            var user = await _userManager.FindByEmailAsync(request.email) ?? throw new NotFoundException(nameof(User), request.email);

            var role = await _roleManager.FindByNameAsync(request.role) ?? throw new NotFoundException(nameof(IdentityRole), request.role);

            await _userManager.AddToRoleAsync(user, role.Name!);
        }
    }
}
