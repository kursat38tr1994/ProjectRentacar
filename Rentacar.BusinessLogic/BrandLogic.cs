using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rentacar.DataAccess.Data.Repository.IRepository;
using Rentacar.DataAccess.Dto.BrandDto;
using Rentacar.Models;

namespace Rentacar.BusinessLogic
{
    public class BrandLogic : IBrandLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public BrandLogic(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<BrandDto> GettAll()
        {
            var items = _unitOfWork.Brand.GetAll();
            return _mapper.Map<IEnumerable<BrandDto>>(items);
        }

        public BrandDto Upsert(BrandDto createbranddto)
        {
            var brandModel = _mapper.Map<Brand>(createbranddto);
            if (createbranddto.Id == 0)
            {
                _unitOfWork.Brand.Add(brandModel);
            }
            else
            {
                _unitOfWork.Brand.Update(brandModel);
            }

            _unitOfWork.Save();
            
            return _mapper.Map<BrandDto>(brandModel);
        }

        public BrandDto GetId(int? id)
        {
            var items = _unitOfWork.Brand.Get(id.GetValueOrDefault());

            return _mapper.Map<BrandDto>(items);
        }
        
        public BrandDto Delete(int? id)
        {
            var items = _unitOfWork.Brand.Get(id.GetValueOrDefault());

            var map = _mapper.Map<BrandDto>(items);

            _unitOfWork.Brand.Remove(items);
            _unitOfWork.Save();
            return map;
        }

        public IEnumerable<SelectListItem> GetDropDown()
        {
          var obj =  _unitOfWork.Brand.GetBrandListItemsForDropDown();
          return _mapper.Map<IEnumerable<SelectListItem>>(obj);
        }
    }
}