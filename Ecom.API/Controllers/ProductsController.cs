using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Enitites.Product;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
   
    public class ProductsController : BaseController
    {
        public ProductsController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {

        }

        [HttpGet("get-all")]
        public async Task<IActionResult> get()
        {
            try
            {
                var products = await work.ProductRepositry
                    .GetAllAsync(x => x.Category, x => x.Photo);

                var result = mapper.Map<List<ProductDTO>>(products);

                if (products is null)
                    return NotFound(new ResponseAPI(404, "no products found"));

                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> getById(int id)
        {
            try
            {
                var product = await work.ProductRepositry.GetByIdAsync(id, x => x.Category, x => x.Photo);

                var result = mapper.Map<ProductDTO>(product);

                if (product is null)
                    return NotFound(new ResponseAPI(404, "product not found"));

                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> add([FromForm] AddProductDTO productDTO)
        {
            try
            {
                await work.ProductRepositry.AddAsync(productDTO);
                return Ok(new ResponseAPI(200));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-product")]
        public async Task<IActionResult> update([FromForm] UpdateProductDTO updateProductDTO)
        {
            try
            {
                await work.ProductRepositry.UpdateAsync(updateProductDTO);
                return Ok(new ResponseAPI(200));
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }

        [HttpDelete("delete-product/{id}")]
        public async Task<IActionResult> delete(int id)
        {
            try
            {
                var product = await work.ProductRepositry.GetByIdAsync(id, x => x.Category, x => x.Photo);
                await work.ProductRepositry.DeleteAsync(product);
                return Ok(new ResponseAPI(200, "product deleted successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }


    }
}
