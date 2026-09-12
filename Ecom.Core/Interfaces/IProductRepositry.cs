using Ecom.Core.DTO;
using Ecom.Core.Enitites.Product;
using Ecom.Core.Sharing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Interfaces
{
    public interface IProductRepositry : IGenericRepositry<Product>
    {
        // for futuer
        Task<ReturnProductDTO> GetAllAsync(ProductParams productParams);
        Task<bool> AddAsync(AddProductDTO productDTO);
        Task<bool> UpdateAsync(UpdateProductDTO updateProductDTO);
        Task DeleteAsync(Product product);
    }
}
