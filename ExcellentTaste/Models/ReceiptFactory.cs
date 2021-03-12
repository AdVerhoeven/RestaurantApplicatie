using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcellentTaste.Models
{
    public class ReceiptFactory
    {
        public Receipt Generate(Order order)
        {

            //Make receipt from products in the order.
            var sb = new StringBuilder();
            decimal total = 0;
            foreach (var orderitem in order.Items)
            {
                //TODO: Clean up and improve.
                var amount = orderitem.Quantity;
                var product = orderitem.Product;
                if (product == null)
                {
                    //TODO: Get the correct product from the database or throw an exception
                }
                sb.Append($"{product.Name} €{product.Price} x{amount}\n");
                total += product.Price * amount;
            }

            return new Receipt()
            {
                CreationTime = DateTime.Now,
                Details = sb.ToString(),
                Total = total
            };
        }
    }
}
