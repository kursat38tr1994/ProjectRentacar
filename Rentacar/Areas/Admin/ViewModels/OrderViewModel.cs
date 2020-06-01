using System.Collections.Generic;
using Rentacar.Models;

namespace Rentacar.Areas.Admin.ViewModels
{
    public class OrderViewModel
    {
        public Rent Rent { get; set; }
        public IEnumerable<RentDetails> OrderDetails { get; set; }
    }
}