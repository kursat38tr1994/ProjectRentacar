namespace Rentacar.DataAccess.Dto.CarDto
{
    public class CarDto
    {
        public int Id { get; set; }
        
        public int BrandId { get; set; }
        
        public BrandDto.BrandDto Brand { get; set; }
        
        public string LicensePlate { get; set; }

        public int FuelId { get; set; }

        public FuelDto.FuelDto Fuel { get; set; }

        public double CurrentPrice { get; set; }
        
        public bool Availability { get; set; }
        
    }
}