using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domin.Entities;
using Restaurants.Domin.Repositories;
using Xunit;


namespace Restaurants.Infrastructure.Authorization.Requirements.Tests
{
    public class CreatedMultipleRestaurantsRequirementHandlerTests
    {
        [Fact()]
        public async Task HandleRequirementAsync_UserHasNotCreatedMultipleRestaurants_ShouldFail()

        {
            var _userContextMock = new Mock<IUserContext>();
            var currentUser = new CurrentUser("1", "test@test.com", [], null, null);
            _userContextMock.Setup(t => t.GetCurrentUser()).Returns(currentUser);
            var _restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();
            var restaurants = new List<Restaurant>()
            {
                new()
                {
                    OwnerId = currentUser.Id,
                },
                new()
                {
                    OwnerId = "2",
                },
            };
            _restaurantsRepositoryMock.Setup(t => t.GetAllAsync()).ReturnsAsync(restaurants);

            var requirement = new CreatedMultipleRestaurantsRequirement(2);
            var handler = new CreatedMultipleRestaurantsRequirementHandler(_restaurantsRepositoryMock.Object,
                _userContextMock.Object);

            var context = new AuthorizationHandlerContext([requirement], null!, null);

            // act

            await handler.HandleAsync(context);

            //assert 

            context.HasSucceeded.Should().BeFalse();
            context.HasFailed.Should().BeTrue();

        }

        [Fact()]
        public async Task HandleRequirementAsync_UserHasCreatedMultipleRestaurants_ShouldSucceed()
        {
            // arrange

            var currentUser = new CurrentUser("1", "test@test.com", [], null, null);
            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(m => m.GetCurrentUser()).Returns(currentUser);

            var restaurants = new List<Restaurant>()
            {
                new()
                {
                    OwnerId = currentUser.Id,
                },
                new()
                {
                    OwnerId = currentUser.Id,
                },
                new()
                {
                    OwnerId = "2",
                },
            };

            var restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();
            restaurantsRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(restaurants);

            var requirement = new CreatedMultipleRestaurantsRequirement(5);
            var handler = new CreatedMultipleRestaurantsRequirementHandler(restaurantsRepositoryMock.Object,
                userContextMock.Object);
            var context = new AuthorizationHandlerContext([requirement], null!, null);

            // act

            await handler.HandleAsync(context);

            // assert

            context.HasSucceeded.Should().BeTrue(); 
            context.HasFailed.Should().BeFalse();

        }
    }
}