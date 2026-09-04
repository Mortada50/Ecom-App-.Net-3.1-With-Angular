using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Ecom.Core.Enitites.Product
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public virtual List<Photo> Photo { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]

        public virtual Category Category { get; set; }
    }
}
