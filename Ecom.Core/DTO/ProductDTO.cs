using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.DTO
{
   
    public class ProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
        public virtual List<PhotoDTO> Photo { get; set; }
        public string CategoryName { get; set; }
    }

    public class PhotoDTO
    {
        public string ImageName { get; set; }
        public int ProductId { get; set; }
    }

    public class AddProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
        public int CategoryId { get; set; }
        public IFormFileCollection Photo { get; set; }
    }

    public class UpdateProductDTO : AddProductDTO
    {
        public int Id { get; set; }
    }
}
