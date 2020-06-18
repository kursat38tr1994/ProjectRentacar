using System.Collections.Generic;
using AutoMapper;
using Rentacar.DataAccess.Data.Repository.IRepository;
using Rentacar.DataAccess.Dto.RentDetailsDto;
using Rentacar.DataAccess.Dto.RentDto;
using Rentacar.Models;

namespace Rentacar.BusinessLogic.Order
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IEnumerable<RentDto> rent;

        public OrderLogic(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public RentDto GetAllRent(int id)
        {
            var rentObj = _unitOfWork.Rent.GetFirstOrDefault(u => u.Id == id,  "User");
            var mapper = _mapper.Map<RentDto>(rentObj);
            return mapper;
        }

        public IEnumerable<RentDetailsDto> GettAllRentDetails(int id)
        {
            var rentDetailsObj = _unitOfWork.RentDetails.GetAll(o => o.Id == id, includeProperties: "Car");
            var mapper = _mapper.Map<IEnumerable<RentDetailsDto>>(rentDetailsObj);
            return mapper;
        }

        public IEnumerable<RentDto> AdminRentData()
        {
            var rentAdminObj = _unitOfWork.Rent.GetAll(includeProperties: "User");
            rent = _mapper.Map<IEnumerable<RentDto>>(rentAdminObj) ;

            return rent;
        }

        public IEnumerable<RentDto> UserRentData(string id)
        {
            var rentAdminObj = _mapper.Map<IEnumerable<RentDto>>(_unitOfWork.Rent.GetAll(u => u.UserId == id,
                    includeProperties: "User"));

            rent = _mapper.Map<IEnumerable<RentDto>>(rentAdminObj);

            return rent;
        }
    }
}