using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Rentacar.DataAccess.Dto.CarDto;
using Rentacar.DataAccess.Dto.RentDto;
using Rentacar.DataAccess.Dto.UserDto;
using Rentacar.Models;

namespace Rentacar.Areas.Customer.ViewModels
{
    public class ShoppingCartViewModel
    {
        public IList<CarDto> ListCar { get; set; }

        public CarDto Car { get; set; }
        
        public int CarId { get; set; }
        public RentDto Rent { get; set; }
        
        public int Id { get; set; }

        public string UserId { get; set; }
        public UserDto User { get; set; }

        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        




    }
}
