using AutoMapper;
using Restaurants.Application.Dishs.Commands.CreateDish;
using Restaurants.Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishs.Dtos
{
    public class DishProfile : Profile
    {
        public DishProfile()
        {
            
            CreateMap<CreateDishCommand, Dish>();
            CreateMap<Dish, DishDto>();
        }
    }

}
