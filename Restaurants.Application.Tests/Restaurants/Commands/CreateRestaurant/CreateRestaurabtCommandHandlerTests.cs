using AutoMapper;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domin.Entities;
using Restaurants.Domin.Repositories;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests
{
    public class CreateRestaurabtCommandHandlerTests
    {
        [Fact()]
        public async Task HandleTest()
        {
            //arrange
            var mockRestaurantRepo = new Mock<IRestaurantsRepository>();
            mockRestaurantRepo.Setup(t => t.CreateAsync(It.IsAny<Restaurant>())).ReturnsAsync(1);
            var mockLogger = new Mock<ILogger<CreateRestaurabtCommandHandler>>();
            var mockMapper = new Mock<IMapper> ();
            var command = new CreateRestaurantCommand();
            var restaurant = new Restaurant();
            mockMapper.Setup(t => t.Map<Restaurant>(command)).Returns(restaurant);
            var mockUserContext = new Mock<IUserContext>();
            var currentUser = new CurrentUser("owner-id", "test@test.com", [], null, null);
            mockUserContext.Setup(t=>t.GetCurrentUser()).Returns(currentUser);
           var CommandHandler = new CreateRestaurabtCommandHandler(mockRestaurantRepo.Object,mockLogger.Object, mockMapper.Object, mockUserContext.Object);

            //act

           var result = await CommandHandler.Handle(command, CancellationToken.None);

            // assert
            result.Should().Be(1);
            restaurant.OwnerId.Should().Be("owner-id");
            mockRestaurantRepo.Verify(r => r.CreateAsync(restaurant), Times.Once);
        }
    }
}