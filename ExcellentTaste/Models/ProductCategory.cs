using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExcellentTaste.Models
{
    public class ProductCategory
    {
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public Category Category { get; set; }
        public Product Product { get; set; }
    }
}
