using AutoMapper;
using Rentacar.DataAccess.Data.Repository.IRepository;
using Rentacar.DataAccess.Dto.RentDetailsDto;
using Rentacar.Models;

namespace Rentacar.BusinessLogic.Rent
{
    public class RentDetailsLogic : IRentDetailsLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RentDetailsLogic(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        
        
        public RentDetailsDto AddDetail(RentDetailsDto model)
        {
            var rentDetails = _mapper.Map<RentDetails>(model);
            
            _unitOfWork.RentDetails.Add(rentDetails);
            
            _unitOfWork.Save();
            
            return model;
        }
    }
}