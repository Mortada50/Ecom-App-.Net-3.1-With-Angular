using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Interfaces
{
    public interface IUnitOfWork
    {
         ICategoryRepositry CategoryRepositry { get; }
         IPhotoRepositry PhotoRepositry { get; }
         IProductRepositry ProductRepositry { get; }
    }
}
