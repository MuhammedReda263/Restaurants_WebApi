using MediatR;
using Restaurants.Application.Dishs.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishs.Queries.GetDishsForRestaurant
{
    public class GetDishsForRestaurantQuery (int id) : IRequest<IEnumerable<DishDto>>
    {
        public int Id { get;} = id;
    }
}
