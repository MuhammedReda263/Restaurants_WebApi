using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domin.Entities;
using Restaurants.Domin.Exceptions;
using Restaurants.Domin.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishs.Commands.CreateDish
{
    internal class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> _logger , IRestaurantsRepository _restaurantsRepository, IDishsRepository _dishsRepository,IMapper _mapper) : IRequestHandler<CreateDishCommand,int>
    {
        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating new dish : {@DishRequest}", request);
            var restaursnt = await _restaurantsRepository.GetByIdAsync(request.RestauratId);
            if (restaursnt == null)
             throw new NotFoundException (nameof(restaursnt),request.RestauratId.ToString());
            var dish = _mapper.Map<Dish>(request);
            return await _dishsRepository.CreateAsync(dish);
            
        }
    }
}
