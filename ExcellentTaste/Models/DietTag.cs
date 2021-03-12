using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExcellentTaste.Models
{
    /// <summary>
    /// Diet info (Gluten-free, Vegan, Vegetarian, Lactose-free, etc.)
    /// </summary>
    public class DietTag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        public ICollection<ProductTag> ProductTags { get; set; }
    }
}
