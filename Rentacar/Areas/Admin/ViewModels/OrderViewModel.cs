using System.Collections.Generic;
using Rentacar.DataAccess.Dto.RentDetailsDto;
using Rentacar.DataAccess.Dto.RentDto;
using Rentacar.Models;

namespace Rentacar.Areas.Admin.ViewModels
{
    public class OrderViewModel
    {
        public RentDto Rent { get; set; }
        public IEnumerable<RentDetailsDto> RentDetails { get; set; }
    }
}