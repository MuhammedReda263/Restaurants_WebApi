
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domin.Entities;
using Restaurants.Domin.Exceptions;
namespace Restaurants.Application.Users.Commands
{
    public class UpdateUserDetailsCommandHandler(ILogger<UpdateUserDetailsCommandHandler> _logger , IUserContext _userContext,IUserStore<User> _userStore) : IRequestHandler<UpdateUserDetailsCommand>
    {
        public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();
            _logger.LogInformation("Upadte user : {UserId} with {@User}", user?.id, request);
            var dbUser = await _userStore.FindByIdAsync(user!.id, cancellationToken);
                if (dbUser == null)
            {
                throw new NotFoundException(nameof(User), user!.id);
            }

            dbUser.Nationality = request.Nationality;
            dbUser.DateOfBirth = request.DateOfBirth;

            await _userStore.UpdateAsync(dbUser, cancellationToken);

        }

       
    }
}
