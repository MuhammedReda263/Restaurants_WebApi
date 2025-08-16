using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishs.Commands.DeleteDishs
{
    public class DeleteDishsForRestaurantCommand (int restaurantId) : IRequest
    {
       public int restaurantId { get; } = restaurantId;
    }
}
