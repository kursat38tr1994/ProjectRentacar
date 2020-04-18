using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Rentacar.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Brand Type")]
        public string Type { get; set; }
    }
}
