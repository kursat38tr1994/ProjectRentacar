namespace Rentacar.DataAccess.Dto.RentDetailsDto
{
    public class RentDetailsDto
    {
     
        public int Id { get; set; }
        
      
        public int RentId { get; set; }


        public RentDto.RentDto Rent { get; set; }
        

        public int CarId { get; set; }
        

        public CarDto.CarDto Car { get; set; }

        public double Price { get; set; }
    }
}