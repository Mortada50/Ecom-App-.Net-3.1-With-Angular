using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Enitites.Product;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Core.Sharing;
using Ecom.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositries
{
    public class ProductRepositry : GenericRepositry<Product>, IProductRepositry
    {
        //private readonly AppDbContext _context;
        private readonly IMapper mapper;
        private readonly IImageManagementService imageManagementService;

        public ProductRepositry(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService) : base(context)
        {
           // _context = context;
            this.mapper = mapper;
            this.imageManagementService = imageManagementService;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync(ProductParams productParams)
        {
             var query =  _context.Products
                .Include(m => m.Category)
                .Include(m => m.Photo)
                .AsNoTracking();
            // filtering by word
            if (!string.IsNullOrEmpty(productParams.Search))
            {
                var searchWords = productParams.Search
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                
                foreach (var word in searchWords)
                {
                    var term = word.Trim();
                    query = query.Where(m => m.Name.Contains(term) ||
                                             m.Description.Contains(term));
                }
            }
            // filtering by category id
            if (productParams.CategoryId.HasValue)
                query = query.Where(m => m.CategoryId == productParams.CategoryId);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "PriceAce":
                            query = query.OrderBy(m => m.NewPrice);
                        break;
                    case "PriceDce":
                        query = query.OrderByDescending(m => m.NewPrice);
                        break;
                    default:
                        query = query.OrderBy(m => m.Name);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(m => m.Name);
            }

            query = query.Skip((productParams.pageSize) * (productParams.PageNumber - 1)).Take(productParams.pageSize);

            var productsList = await query.ToListAsync();
            var result = mapper.Map<List<ProductDTO>>(productsList);
            return result;
        }

        public async Task<bool> AddAsync(AddProductDTO productDTO)
        {
            if (productDTO == null) return false;
            var product = mapper.Map<Product>(productDTO);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            var ImagePath = await imageManagementService.AddImageAsync(productDTO.Photo, productDTO.Name);
            var photo = ImagePath.Select(path => new Photo
            {
                ImageName = path,
                ProductId = product.Id
            }).ToList();
            await _context.Photos.AddRangeAsync(photo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(Product product)
        {
            var photo = await _context.Photos.Where(m => m.Id == product.Id).ToListAsync();

            foreach(var item in photo)
            {
                imageManagementService.DeleteImageAsync(item.ImageName);
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(UpdateProductDTO updateProductDTO)
        {
            if (updateProductDTO is null) return false;
            var FindProduct = await _context.Products.Include(m => m.Category)
                .Include(m => m.Photo)
                .FirstOrDefaultAsync(m => m.Id == updateProductDTO.Id);
            if (FindProduct is null) return false;
            mapper.Map(updateProductDTO, FindProduct);
            var FindPhoto = await _context.Photos.Where(m => m.ProductId == updateProductDTO.Id).ToListAsync();
            foreach(var item in FindPhoto)
            {
                imageManagementService.DeleteImageAsync(item.ImageName);
            }
            _context.Photos.RemoveRange(FindPhoto);
            var ImagePath = await imageManagementService.AddImageAsync(updateProductDTO.Photo, updateProductDTO.Name);
            var photo = ImagePath.Select(path => new Photo
            {
                ImageName = path,
                ProductId = updateProductDTO.Id
            }).ToList();
            await _context.Photos.AddRangeAsync(photo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
