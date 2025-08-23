using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurant
{
    public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
    {
        private int[] allowedSizes = [5,10,15,20,25,30];
        public string[] allowedSortByColumnNames = [nameof(RestaurantDto.Name),
        nameof(RestaurantDto.Category),
        nameof(RestaurantDto.Description)];
        public GetAllRestaurantsQueryValidator()
        {
            RuleFor(temp=>temp.PageNumber).GreaterThanOrEqualTo(1)
                .WithMessage("PageNumber must be greater than 1");
            RuleFor(temp=>temp.PageSize).Must(value=> allowedSizes.Contains(value))
                .WithMessage($"Page Size must be in [{string.Join(",", allowedSizes)}]");
            RuleFor(r => r.SortBy)
           .Must(value => allowedSortByColumnNames.Contains(value))
           .When(q => q.SortBy != null)
           .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowedSortByColumnNames)}]");
        }
    }
}
