using AutoMapper;
using Rentacar.DataAccess.Data.Repository.IRepository;
using Rentacar.DataAccess.Dto.CarDto;
using Rentacar.DataAccess.Dto.UserDto;

namespace Rentacar.BusinessLogic
{
    public class UserDetails : IUserDetails
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserDetails(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public UserDto GetFirstOfDefault(string id)
        {
            var obj = _unitOfWork.User.GetFirstOrDefault(u => u.Id == id);

            return _mapper.Map<UserDto>(obj);
        }

        
    }
}