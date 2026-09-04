using Ecom.Core.Enitites.Product;
using Ecom.Core.Interfaces;
using Ecom.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Repositries
{
    public class CategoryRepositry : GenericRepositry<Category>, ICategoryRepositry
    {
        public CategoryRepositry(AppDbContext context) : base(context)
        {

        }
    }
}
