using Rentacar.DataAccess.Data.Dto.BrandDto;

namespace Rentacar.DataAccess.Data.Dto.CarDto
{
    public class CarDto
    {
        public int Id { get; set; }
        
        public int BrandId { get; set; }
        
        public ReadBrandDto Brand { get; set; }
        
        public string LicensePlate { get; set; }
        
        public string Fuel { get; set; }
        
        public double CurrentPrice { get; set; }
        
        public bool Availability { get; set; }
        
    }
}