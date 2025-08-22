using Xunit;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Domin.Interfaces;
using Restaurants.Domin.Repositories;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Domin.Entities;
using FluentAssertions;
using Restaurants.Domin.Exceptions;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant.Tests
{
    public class UpdateRestaurantCommandHandlerTests
    {
        private readonly Mock<ILogger<CreateRestaurabtCommandHandler>> _loggerMock;
        private readonly Mock<IRestaurantsRepository> _restaurantsRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRestaurantAuthorizationService> _restaurantAuthorizationServiceMock;
        private readonly UpdateRestaurantCommandHandler _handler;

        public UpdateRestaurantCommandHandlerTests()
        {
            _loggerMock = new Mock<ILogger<CreateRestaurabtCommandHandler>>();
            _restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();
            _mapperMock = new Mock<IMapper>();
            _restaurantAuthorizationServiceMock = new Mock<IRestaurantAuthorizationService>();

            _handler = new UpdateRestaurantCommandHandler(               
                _restaurantsRepositoryMock.Object,
                _loggerMock.Object,
                _mapperMock.Object,
                _restaurantAuthorizationServiceMock.Object);
        }

        [Fact()]
        public async Task Handle_WithValidRequest_ShouldUpdateRestaurants()
        {
            //arrange
            var restaurantId = 1;
            var command = new UpdateRestaurantCommand()
            {
                Id = restaurantId,
                Name = "New Test",
                Description = "New Description",
                HasDelivery = true,
            };

            var restaurant = new Restaurant()
            {
                Id = restaurantId,
                Name = "Test",
                Description = "Test",
            };

            _restaurantsRepositoryMock.Setup(r => r.GetByIdAsync(restaurantId))
                .ReturnsAsync(restaurant);

            _restaurantAuthorizationServiceMock.Setup(m => m.Authorize(restaurant, Domin.Constants.ResourceOperation.Update))
          .Returns(true);

            //act

            await _handler.Handle(command,CancellationToken.None);


            //assert.

            _restaurantsRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
            _mapperMock.Verify(x => x.Map(command, restaurant), Times.Once);


        }

        [Fact]
        public async Task Handle_WithNonExistingRestaurant_ShouldThrowNotFoundException()
        {
            // Arrange
            var restaurantId = 2;
            var request = new UpdateRestaurantCommand
            {
                Id = restaurantId
            };

            _restaurantsRepositoryMock.Setup(r => r.GetByIdAsync(restaurantId))
                    .ReturnsAsync((Restaurant?)null);

            // act

            Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

            // assert
            await act.Should().ThrowAsync<NotFoundException>()
                    .WithMessage($"Restaurant with id: {restaurantId} doesn't exist");
        }

        [Fact]
        public async Task Handle_WithUnauthorizedUser_ShouldThrowForbidException()
        {
            var restaurantId = 1;
            var command = new UpdateRestaurantCommand()
            {
                Id = restaurantId,
                Name = "New Test",
                Description = "New Description",
                HasDelivery = true,
            };

            var restaurant = new Restaurant()
            {
                Id = restaurantId,
                Name = "Test",
                Description = "Test",
            };

            _restaurantsRepositoryMock.Setup(r => r.GetByIdAsync(restaurantId))
                .ReturnsAsync(restaurant);

            _restaurantAuthorizationServiceMock.Setup(m => m.Authorize(restaurant, Domin.Constants.ResourceOperation.Update))
          .Returns(false);

            //act

            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);


            //assert

            await act.Should().ThrowAsync<ForbidException>();
        }



    }
}