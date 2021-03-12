using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExcellentTaste.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }

        public Order Order { get; set; }
        public int Quantity { get; set; }

        public bool IsReady { get; set; }
    }
}
