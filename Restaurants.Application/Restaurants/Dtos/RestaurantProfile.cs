using AutoMapper;
using Restaurants.Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Dtos
{
    public class DishProfile : Profile
    {
        public DishProfile()
        { 
            CreateMap<Restaurant,RestaurantDto>()
                .ForMember(d=>d.City,opt=>opt.MapFrom(src=> src.Address == null ? null : src.Address.City))
                .ForMember(d=>d.Street,opt=>opt.MapFrom(src=>src.Address == null ? null : src.Address.Street))
                .ForMember(d => d.PostalCode, opt =>opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
                .ForMember(d=>d.Dishes,opt=>opt.MapFrom(src=>src.Dishes));
            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(d => d.Address, opt => opt.MapFrom(src => new Address { City = src.City, Street = src.Street, PostalCode = src.PostalCode }));
               



        }
    }
}
