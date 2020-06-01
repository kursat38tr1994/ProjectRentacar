using Microsoft.AspNetCore.Identity;
using Rentacar.Areas.Admin.ViewModels;
using Rentacar.DataAccess.Data.Repository;
using Rentacar.DataAccess.Dto.BrandDto;
using Rentacar.Models;

namespace Rentacar.Profile
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<SignUpViewModel, User>();
            CreateMap<User, SignUpViewModel>();
            
            CreateMap<SignInViewModel, User>();
            CreateMap<User, SignInViewModel>();
        }
    }
}
