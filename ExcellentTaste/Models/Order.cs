using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExcellentTaste.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime? StartTime { get; set; }

        public ICollection<OrderTable> OrderTables { get; set; }

        public IList<OrderItem> Items { get; set; }
    }
}
