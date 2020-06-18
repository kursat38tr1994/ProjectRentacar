using System.Collections.Generic;
using System.Dynamic;
using AutoMapper;
using Rentacar.DataAccess.Data.Repository.IRepository;
using Rentacar.DataAccess.Dto.CarDto;
using Rentacar.Models;

namespace Rentacar.BusinessLogic
{
    public class CarLogic : ICarLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CarLogic(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public CarDto GetFirstOfDefault(int? id)
         {
             var obj = _unitOfWork.Car.GetFirstOrDefault(u => u.Id == id, includeProperties:"Brand,Fuel");

             return _mapper.Map<CarDto>(obj);
         }
        
        
        public IEnumerable<CarDto> GetAll()
        {
            var obj = _unitOfWork.Car.GetAll(car=> car.Availability, includeProperties:"Brand,Fuel");

            return _mapper.Map<IEnumerable<CarDto>>(obj);
        }



        public CarDto GetId(int? id)
        {
            var obj = _unitOfWork.Car.Get(id.GetValueOrDefault());

            return _mapper.Map<CarDto>(obj);
        }

        public CarDto Upsert(CarDto carDto)
        {
            var carModel = _mapper.Map<Car>(carDto);
            if (carDto.Id == 0)
            {
                _unitOfWork.Car.Add(carModel);
            }
            else
            {
                _unitOfWork.Car.Update(carModel);
            }
            _unitOfWork.Save();

            return _mapper.Map<CarDto>(carModel);
        }

        
        public CarDto Delete(int? id)
        {
            var obj = _unitOfWork.Car.Get(id.GetValueOrDefault());

            var map = _mapper.Map<CarDto>(obj);
            
            _unitOfWork.Car.Remove(obj);

            _unitOfWork.Save();

            return map;
        }


        public void ChangeAvailability(CarDto carModel)
        {
            
            var mapper = _mapper.Map<Car>(carModel);

            _unitOfWork.Car.Update(mapper);
            _unitOfWork.Save();
        }
    }
        
}