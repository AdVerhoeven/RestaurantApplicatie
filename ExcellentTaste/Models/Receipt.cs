using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcellentTaste.Models
{
    /// <summary>
    /// Should be immutable once made, finishes up the order and saves this to the database with fixed values...
    /// </summary>
    public class Receipt
    {
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreationTime { get; set; }

        public string Details { get; set; }

        public decimal Total { get; set; }
    }
}
