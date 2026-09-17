using AutoMapper;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.infrastructure.Data;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Repositries
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IMapper mapper;
        private readonly IImageManagementService imageManagementService;
        private readonly IConnectionMultiplexer redis;

        public ICategoryRepositry CategoryRepositry { get; }

        public IPhotoRepositry PhotoRepositry { get; }

        public IProductRepositry ProductRepositry { get; }

        public ICustomerBasketRepositry CustomerBasket { get; }

        public UnitOfWork(
            AppDbContext context,
            IMapper mapper,
            IImageManagementService imageManagementService,
            IConnectionMultiplexer redis
            )
        {
            _context = context;
            this.mapper = mapper;
            this.imageManagementService = imageManagementService;
            this.redis = redis;
            CategoryRepositry = new CategoryRepositry(_context);
            PhotoRepositry = new PhotoRepositry(_context);
            ProductRepositry = new ProductRepositry(_context, mapper, imageManagementService);
            CustomerBasket = new CustomerBasketRepositry(redis);
        }


    }
}
