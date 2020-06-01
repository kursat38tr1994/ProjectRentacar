using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Rentacar.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string LicensePlate { get; set; }

        
        [Required]
        public int BrandId { get; set; }

        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }


        [Required]
        public int FuelId { get; set; }

        [ForeignKey("FuelId")]
        public Fuel Fuel { get; set; }

        [Required]
        [Range(1, 10000)]
        public double CurrentPrice { get; set; }

        [Required]
        public bool Availability { get; set; }

        public string ImageUrl { get; set; }
    }
}
