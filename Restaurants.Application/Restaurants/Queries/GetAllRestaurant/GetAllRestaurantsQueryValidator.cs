using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurant
{
    public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
    {
        public int[] allowedSizes = [5,10,15,20,25,30];
        public GetAllRestaurantsQueryValidator()
        {
            RuleFor(temp=>temp.PageNumber).GreaterThanOrEqualTo(1)
                .WithMessage("PageNumber must be greater than 1");
            RuleFor(temp=>temp.PageSize).Must(value=> allowedSizes.Contains(value))
                .WithMessage($"Page Size must be in [{string.Join(",", allowedSizes)}]");
        }
    }
}
