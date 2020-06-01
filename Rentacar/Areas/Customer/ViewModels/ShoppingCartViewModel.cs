using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Rentacar.Models;

namespace Rentacar.Areas.Customer.ViewModels
{
    public class ShoppingCartViewModel
    {
        
        public IEnumerable<ShoppingCart> ListCart { get; set; }

        public Rent Rent { get; set; }
        
        public int Id { get; set; }

        public string UserId { get; set; }
      
        public User User { get; set; }

        public int CarId { get; set; }
        
        public Car Car { get; set; }
        
    }
}
