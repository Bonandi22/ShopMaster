using AutoMapper;
using IdentityService.Application.DTOs;
using IdentityService.Domain.Entities;

namespace IdentityService.Application.Mappings
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<User, UserDto>().ReverseMap(); ;
            CreateMap<RegisterDto, User>().ReverseMap(); ;
            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}