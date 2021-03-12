using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExcellentTaste.Models
{
    public class OrderTable
    {
        public DateTime Reservation { get; set; }
        public Table Table { get; set; }

        public int TableId { get; set; }
        public Order Order { get; set; }

        public int OrderId { get; set; }
    }
}
