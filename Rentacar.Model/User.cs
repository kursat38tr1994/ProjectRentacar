using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Rentacar.Models
{
    public class User : IdentityUser, IUser
    {
        [Required]
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Residence { get; set; }
        public string Postalcode { get; set; }
        public string Housenumber { get; set; }

        [NotMapped]
        public string Role { get; set; }

    }
}