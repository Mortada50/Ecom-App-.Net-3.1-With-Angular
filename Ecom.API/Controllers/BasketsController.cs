using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.Enitites;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    
    public class BasketsController : BaseController
    {
        public BasketsController(IUnitOfWork work, IMapper mapper) : base(work, mapper) { }

        [HttpGet("get-basket-item/{id}")]
        public async Task<IActionResult>get(string id)
        {
            var result = await work.CustomerBasket.GetBasketAsync(id);
            if (result is null)
            {
                return Ok(new CustomerBasket());
            }
            return Ok(result);
        }

        [HttpPost("update-basket")]
        public async Task<IActionResult>add(CustomerBasket basket)
        {
            var _basket = await work.CustomerBasket.UpdateBasketAsync(basket);
           
            return Ok(basket);
        }

        [HttpDelete("delete-basket-item/{id}")]
        public async Task<IActionResult>delete(string id)
        {
            var result = await work.CustomerBasket.DeleteBasketAsync(id);
            if (result) return Ok(new ResponseAPI(200, "item deleted"));

            return  BadRequest(new ResponseAPI(400));
        }
    }
}
