// using System.Collections.Generic;
// using Microsoft.AspNetCore.Http;
// using Rentacar.Utility;
// using Microsoft.AspNetCore.Http; 
//
// namespace Rentacar.Extensions
// {
//     public static class SessionCheck
//     {
//         public static void GetSession(this HttpContext session)
//         {
//             
//             if (HttpContext.GetObject<List<int>>(Session.SessionCart) != null)
//             {
//                 var sessions = new List<int>();
//                 sessions = HttpContext.Session.GetObject<List<int>>(Session.SessionCart);
//
//                 foreach (var carId in sessions)
//                 {
//                     // var getCarFromDb = _carLogic.GetFirstOfDefault(carId);
//                     // var mapperToCar = _mapper.Map<CarViewModel>(getCarFromDb);
//                 }
//             }
//         }
//     }
// }