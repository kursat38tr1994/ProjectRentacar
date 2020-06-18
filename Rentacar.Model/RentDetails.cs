using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rentacar.Models
{
    public class RentDetails
    {
        [Key] 
        public int Id { get; set; }
        
        [Required] 
        public int RentId { get; set; }

        [ForeignKey("RentId")] 
        public Rent Rent { get; set; }
        
        [Required] 
        public int CarId { get; set; }
        
        [ForeignKey("CarId")] 
        public Car Car { get; set; }
        
        [Required] 
        public double Price { get; set; }
    }
}