using System.Collections.Generic;
using AutoMapper;
using Rentacar.DataAccess.Data.Dto.BrandDto;
using Rentacar.DataAccess.Data.Repository.IRepository;
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

        public IEnumerable<readbranddto> GettAll()
        {
            var items = _unitOfWork.Brand.GetAll();

            return _mapper.Map<IEnumerable<readbranddto>>(items);
        }

        public readbranddto CreateBrand(createbranddto createbranddto)
        {
            var brandModel = _mapper.Map<Brand>(createbranddto);
            _unitOfWork.Brand.Add(brandModel);
            _unitOfWork.Save();
            var brandReadDto = _mapper.Map<readbranddto>(brandModel);

            return brandReadDto;
        }

        public readbranddto GetId(int? id)
        {
            var items = _unitOfWork.Brand.Get(id.GetValueOrDefault());
            
            return _mapper.Map<readbranddto>(items); 
          
        }
    }
}