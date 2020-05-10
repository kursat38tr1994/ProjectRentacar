using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Rentacar.Models
{
    public class Fuel
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Fuel Type")]
        public string Type { get; set; }
    }
}
