using MediatR;
using Restaurants.Application.Dishs.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishs.Queries.GetDishByIdForRestaurant
{
    public class GetDishByIdForRestaurantQuery (int RestaurabtId,int DishId) : IRequest<DishDto>
    {
        public int RestaurabtId { get; } = RestaurabtId;
        public int  DishId { get;} = DishId;
    }
}
