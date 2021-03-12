using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExcellentTaste.Models
{
    public class Table
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<OrderTable> OrderTables { get; set; }

        public int Seats { get; set; }
    }
}
