using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishs.Commands.CreateDish
{
    public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
    {
        public CreateDishCommandValidator() { 
        //RuleFor(prop=>prop.Name).NotEmpty();
        //RuleFor(prop=>prop.Description).NotEmpty();
        RuleFor(prop=>prop.Price).NotEmpty().GreaterThanOrEqualTo(0).WithMessage("Price must be a non-negative number.");
        RuleFor(prop=>prop.KiloCalories).GreaterThanOrEqualTo(0).WithMessage("KiloCalories must be a non-negative number.");
          



        }
    }
}
