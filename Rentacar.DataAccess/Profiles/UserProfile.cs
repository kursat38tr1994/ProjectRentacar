using AutoMapper;
using Rentacar.DataAccess.Dto.ShoppingCartDto;
using Rentacar.DataAccess.Dto.UserDto;
using Rentacar.Models;

namespace Rentacar.DataAccess.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>();
            
            CreateMap<User, UserDto>();
        }
    }
}