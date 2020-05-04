using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<ReadBrandDto> GettAll()
        {
            var items = _unitOfWork.Brand.GetAll();
            return _mapper.Map<IEnumerable<ReadBrandDto>>(items);
        }

        public ReadBrandDto Upsert(ReadBrandDto createbranddto)
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
            
            return _mapper.Map<ReadBrandDto>(brandModel);
        }

        public ReadBrandDto GetId(int? id)
        {
            var items = _unitOfWork.Brand.Get(id.GetValueOrDefault());

            return _mapper.Map<ReadBrandDto>(items);
        }
        
        public ReadBrandDto Delete(int? id)
        {
            var items = _unitOfWork.Brand.Get(id.GetValueOrDefault());

            return _mapper.Map<ReadBrandDto>(items);
        }
    }
}