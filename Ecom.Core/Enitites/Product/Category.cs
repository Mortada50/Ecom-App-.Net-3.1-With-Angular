using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Enitites.Product
{
    public class Category: BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Product> products { get; set; } = new HashSet<Product>();
    }
}
