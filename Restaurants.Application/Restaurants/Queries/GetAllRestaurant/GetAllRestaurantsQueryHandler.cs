using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domin.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurant
{
    public class GetAllRestaurantsQueryHandler(IRestaurantsRepository _restaurantsRepo, ILogger<GetAllRestaurantsQueryHandler> logger, IMapper mapper) : IRequestHandler<GetAllRestaurantsQuery, PageResult<RestaurantDto>>
    {
       public async Task<PageResult<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all restaurants");
            var (restaurants,total) = await _restaurantsRepo.GetAllMatchingAsync(request.SearchPhrase,request.PageNumber,request.PageSize,request.SortBy,request.SortDirection);
            var restaurantsDto = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
            var PageResult = new PageResult<RestaurantDto>(restaurantsDto,total,request.PageSize,request.PageNumber);
            return PageResult;

        }
    }
}
