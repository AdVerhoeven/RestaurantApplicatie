using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExcellentTaste.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [DisplayName("Allergen/Diet Info")]
        public ICollection<ProductTag> ProductTags { get; set; }

        public ICollection<ProductCategory> Category { get; set; }

        [DisplayFormat()]
        public decimal Price { get; set; }

        public decimal Tax { get; set; }

        public bool Available { get; set; }
    }
}
