using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Enitites.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecom.API.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap< Product, ProductDTO >()
                .ForMember(x => x.CategoryName, op => op.MapFrom(src => src.Category.Name))
                .ReverseMap();

            CreateMap<Photo, PhotoDTO>().ReverseMap();
            CreateMap<AddProductDTO, Product>()
                .ForMember(m => m.Photo, op => op.Ignore())
                .ReverseMap();
            CreateMap<UpdateProductDTO, Product>()
               .ForMember(m => m.Photo, op => op.Ignore())
               .ReverseMap();
        }
    }
}
