using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExcellentTaste.Models
{
    public class CategoryViewModel
    {
        public List<Product> Products { get; set; }

        public string Name { get; set; }
        public int Id { get; set; }
        public CategoryViewModel(Category category)
        {
            Products = category.Products.Select(p => p.Product).ToList();
            Name = category.Name;
            Id = category.Id;
        }
    }
}
