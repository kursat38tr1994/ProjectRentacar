// using System.Collections.Generic;
// using System.Dynamic;
// using AutoMapper;
// using Microsoft.AspNetCore.Mvc;
// using Rentacar.BusinessLogic.Interface;
// using Rentacar.DataAccess.Data.Repository.IRepository;
// using Rentacar.DataAccess.Dto.CarDto;
// using Rentacar.DataAccess.Dto.ShoppingCartDto;
// using Rentacar.Models;
//
// namespace Rentacar.BusinessLogic
// {
//     public class ShoppingCartLogic : IShoppingCartLogic
//     {
//         private readonly IUnitOfWork _unitOfWork;
//         private readonly IMapper _mapper;
//
//         [BindProperty] 
//         private ShoppingCartDto ShoppingCart { get; set; }
//
//
//         public ShoppingCartLogic(IUnitOfWork unitOfWork, IMapper mapper)
//         {
//             _unitOfWork = unitOfWork;
//             _mapper = mapper;
//         }
//
//
//         public ShoppingCartDto GetFirstOfDefault(int? id)
//         {
//             var obj = _unitOfWork.Car.GetFirstOrDefault(u => u.Id == id);
//             var mapper = _mapper.Map<ShoppingCartDto>(obj);
//             ShoppingCart = new ShoppingCartDto
//             {
//                 Car = mapper,
//                 CarId = obj.Id,
//             };
//
//             return _mapper.Map<ShoppingCartDto>(obj);
//         }
//         
//         
//     }
// }