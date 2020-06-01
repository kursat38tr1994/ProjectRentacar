using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Rentacar.DataAccess.Dto.UserDto
{
    public class UserDto : IdentityUser
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Residence { get; set; }
        public string Postalcode { get; set; }
        public string Housenumber { get; set; }
        
        public string Role { get; set; }
    }
}