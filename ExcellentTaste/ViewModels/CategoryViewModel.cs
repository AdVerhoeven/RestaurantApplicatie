using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExcellentTaste.Models
{
    public class CategoryViewModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public CategoryViewModel(Category category)
        {
            Name = category.Name;
            Id = category.Id;
        }
    }
}
