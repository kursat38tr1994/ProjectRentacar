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
        private readonly ICarLogic _carLogic;

        public RentDetailsLogic(IUnitOfWork unitOfWork, IMapper mapper, ICarLogic carLogic)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _carLogic = carLogic;
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