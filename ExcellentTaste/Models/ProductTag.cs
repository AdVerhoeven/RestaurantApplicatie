using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExcellentTaste.Models
{
    public class ProductTag
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int DietTagId { get; set; }
        public DietTag DietTag { get; set; }
    }
}
