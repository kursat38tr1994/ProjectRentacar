using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rentacar.BusinessLogic.Interface;
using Rentacar.DataAccess.Data.Repository.IRepository;
using Rentacar.DataAccess.Dto.FuelDto;
using Rentacar.Models;

namespace Rentacar.BusinessLogic
{
    public class FuelLogic : IFuelLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FuelLogic(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public FuelDto Upsert(FuelDto createFuel)
        {
            var fuelModel = _mapper.Map<Fuel>(createFuel);
            if (createFuel.Id == 0)
            {
                _unitOfWork.Fuel.Add(fuelModel);
            }
            else
            {
                _unitOfWork.Fuel.Update(fuelModel);
            }

            _unitOfWork.Save();

            return _mapper.Map<FuelDto>(fuelModel);
        }


        public FuelDto GetById(int? id)
        {
            var obj = _unitOfWork.Fuel.Get(id.GetValueOrDefault());

            return _mapper.Map<FuelDto>(obj);
        }

        public IEnumerable<FuelDto> GetAll()
        {
            var obj = _unitOfWork.Fuel.GetAll();

            return _mapper.Map<IEnumerable<FuelDto>>(obj);
        }

        public IEnumerable<SelectListItem> GetDropDown()
        {
            var obj = _unitOfWork.Fuel.GetFuelListItemsForDropDown();
            return _mapper.Map<IEnumerable<SelectListItem>>(obj);
        }
    }
}
