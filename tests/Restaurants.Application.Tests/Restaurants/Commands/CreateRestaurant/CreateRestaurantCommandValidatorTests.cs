using Xunit;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.TestHelper;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests
{
    public class CreateRestaurantCommandValidatorTests
    {
        [Fact()]
        public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
        {
            //arrang
            var command = new CreateRestaurantCommand()
            {
                Name = "Test",
                Category = "Italian",
                ContactEmail = "test@test.com",
                PostalCode = "12-345",
            };

            var validator = new CreateRestaurantCommandValidator();
            //act

           var result = validator.TestValidate(command);

            //assert

            result.ShouldNotHaveAnyValidationErrors();
        } 
        
        [Fact()]
        public void Validator_ForInValidCommand_ShouldHaveValidationErrors()
        {
            //arrang
            var command = new CreateRestaurantCommand()
            {
                Name = "Te",
                Category = "Ital",
                ContactEmail = "@test.com",
                PostalCode = "125",
            };

            var validator = new CreateRestaurantCommandValidator();
            //act

           var result = validator.TestValidate(command);

            //assert

            result.ShouldHaveValidationErrorFor(t=>t.Name);
            result.ShouldHaveValidationErrorFor(t => t.Category);
            result.ShouldHaveValidationErrorFor(t=>t.ContactEmail);
            result.ShouldHaveValidationErrorFor(t=>t.PostalCode);
        }



        [Theory()]
        [InlineData("Italian")]
        [InlineData("Mexican")]
        [InlineData("Japanese")]
        [InlineData("American")]
        [InlineData("Indian")]
        public void Validator_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty(string categoryName)
        {
            //arrang
            var command = new CreateRestaurantCommand()
            {
                Category = categoryName,

            };

            var validator = new CreateRestaurantCommandValidator();
            //act

           var result = validator.TestValidate(command);

            //assert

            result.ShouldNotHaveValidationErrorFor(t=>t.Category);

        }

        [Theory()]
        [InlineData("10220")]
        [InlineData("102-20")]
        [InlineData("10 220")]
        [InlineData("10-2 20")]
        public void Validator_ForInvalidPostalCode_ShouldHaveValidationErrorsForPostalCodeProperty(string postalCode)
        {
            //arrang
            var command = new CreateRestaurantCommand()
            {
                PostalCode = postalCode,

            };

            var validator = new CreateRestaurantCommandValidator();
            //act

           var result = validator.TestValidate(command);

            //assert

            result.ShouldHaveValidationErrorFor(t=>t.PostalCode);

        }


    }
}