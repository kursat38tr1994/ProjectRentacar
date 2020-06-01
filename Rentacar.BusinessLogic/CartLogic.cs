using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Rentacar.Models;

namespace Rentacar.BusinessLogic
{
    public class CartLogic
    {
        private readonly ISession _session;

        public CartLogic(ISession session)
        {
            _session = session; 
        }

        
        
        public void Do(ShoppingCart shoppingCart)
        {
            var stringObject = JsonConvert.SerializeObject(shoppingCart);
            
            
            
            _session.SetString("Cart", stringObject);
        }
    }
}