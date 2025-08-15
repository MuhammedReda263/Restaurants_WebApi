using MediatR;
using Restaurants.Application.Restaurants.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Queries.RestaurantById
{
    public class GetRestaurantByIdQuery : IRequest<RestaurantDto>
    {
        public GetRestaurantByIdQuery(int id)
        {
            this.id = id;
        }

        public int id {  get;}
    }
}
