using System.Runtime.InteropServices.ComTypes;
using AutoMapper;
using Rentacar.DataAccess.Data.Repository.IRepository;
using Rentacar.DataAccess.Dto.RentDto;

namespace Rentacar.BusinessLogic.Rent
{
    public class RentLogic : IRentLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICarLogic _carLogic;

        public RentLogic(IUnitOfWork unitOfWork, IMapper mapper, ICarLogic carLogic)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _carLogic = carLogic;
        }




        public RentDto GetFirstOfDefault(RentDto model)
        { 
           var rentObj =  _unitOfWork.Rent.GetFirstOrDefault(rent => rent.Id == model.Id);
           var mapper = _mapper.Map<RentDto>(rentObj);
           return mapper;
        }
        
        public RentDto AddRent(RentDto model)
        {
            var rentObj = _mapper.Map<Models.Rent>(model);

            _unitOfWork.Rent.Add(rentObj);

            var rentId = model.Id = _unitOfWork.Rent.ReturnLast();
            var objRent = _unitOfWork.Rent.GetFirstOrDefault(rent => rent.Id == rentId);
            var mapper = _mapper.Map<RentDto>(objRent);
            model = mapper;

            return model;
        }
    }
}