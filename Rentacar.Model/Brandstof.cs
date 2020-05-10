using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Rentacar.Models
{
    public class Brandstof
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Fuel Type")]
        public string Type { get; set; }
    }
}
