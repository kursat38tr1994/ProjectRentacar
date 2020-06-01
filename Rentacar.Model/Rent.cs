using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rentacar.Models
{
    public class Rent
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        
        public User User { get; set; }

        public DateTime RentDate { get; set; }
        
        public DateTime ReturnDate { get; set; } 

        public double Total { get; set; }

    }
}