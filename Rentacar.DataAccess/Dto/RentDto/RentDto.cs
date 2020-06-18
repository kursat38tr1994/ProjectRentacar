using System;
using System.ComponentModel.DataAnnotations;

namespace Rentacar.DataAccess.Dto.RentDto
{
    public class RentDto
    {
        
        public int Id { get; set; }
        
        
        public string UserId { get; set; }
      
        
        public UserDto.UserDto User { get; set; }

     
        public DateTime RentDate { get; set; }

     
        public DateTime ReturnDate { get; set; } 

        public double Total { get; set; }
    }
}