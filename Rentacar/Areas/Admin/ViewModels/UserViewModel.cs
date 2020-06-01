using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Rentacar.Models;

namespace Rentacar.Areas.Admin.ViewModels
{
    public class UserViewModel 
    {
        public UserViewModel()
        {
            Claims = new List<string>();
            Roles = new List<string>();
        }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Id { get; set; }
        
        public string Phonenumber { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Residence { get; set; }
        public string Postalcode { get; set; }
        public string Housenumber { get; set; }
        
        public List<string> Claims { get; set; }

        public IList<string> Roles { get; set; }
    }
}