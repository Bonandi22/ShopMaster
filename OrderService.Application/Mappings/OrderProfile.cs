using AutoMapper;
using OrderService.Application.DTOs;
using OrderService.Domain.Entities;

namespace OrderService.Application.Mappings
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Cart, CartDto>().ReverseMap();
            CreateMap<Order, CartDto>().ReverseMap();
        }
    }
}