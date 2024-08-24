using AutoMapper;
using ShippingService.Application.DTOs;
using ShippingService.Domain.Entities;

namespace ShippingService.Application.Mappings
{
    public class ShippingProfile : Profile
    {
        public ShippingProfile()
        {
            CreateMap<Shipping, ShippingDto>().ReverseMap();
            CreateMap<Tracking, TrackingDto>().ReverseMap();
        }
    }
}