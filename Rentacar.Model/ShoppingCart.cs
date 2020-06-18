using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rentacar.Models
{
    public class ShoppingCart
    {

        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        
        public User User { get; set; }
        
        public int CarId { get; set; }
        [ForeignKey("CarId")]
        
        public Car Car { get; set; }

        // public DateTime RentDate { get; set; }
        // public DateTime ReturnDate { get; set; }
        
        [Range(1,2, ErrorMessage = "Aub 1 tot en met 10000")]
        public int Count { get; set; }
        
        [NotMapped]
        public double Price { get; set; }
        
        public double ImageUrl { get; set; }
    }
}