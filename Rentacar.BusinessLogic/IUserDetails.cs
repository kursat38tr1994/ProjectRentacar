using System;
using Rentacar.DataAccess.Dto.UserDto;
using Rentacar.Models;

namespace Rentacar.BusinessLogic
{
    public interface IUserDetails
    {
        UserDto GetFirstOfDefault(string id);
        
    }
}