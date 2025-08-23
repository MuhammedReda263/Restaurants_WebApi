using FluentAssertions;
using Restaurants.Domin.Constants;
using Xunit;


namespace Restaurants.Application.Users.Tests
{
    public class CurrentUserTests
    {
        //TestMethod_Scenario_ExpectResult

        [Theory]
        [InlineData(UserRoles.Admin)]
        [InlineData(UserRoles.User)]
        public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
        {
            //arrange

            var currentUser = new CurrentUser("1", "email", [UserRoles.User, UserRoles.Admin, UserRoles.Owner], null,null);

            //act

            var result = currentUser.IsInRole(roleName);

            //assert

            result.Should().BeTrue();
        }
        
        [Fact()]
        public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
        {
            //arrange

            var currentUser = new CurrentUser("1", "email", [UserRoles.User, UserRoles.Admin], null,null);

            //act

            var result = currentUser.IsInRole(UserRoles.Owner);

            //assert

            result.Should().BeFalse();
        }
        
        [Fact()]
        public void IsInRole_WithNOMatchingRoleCase_ShouldReturnFalse()
        {
            //arrange

            var currentUser = new CurrentUser("1", "email", [UserRoles.User, UserRoles.Admin, UserRoles.Owner], null,null);

            //act

            var result = currentUser.IsInRole(UserRoles.Admin.ToLower());

            //assert

            result.Should().BeFalse();
        }
    }
}