using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExcellentTaste.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ExcellentTaste.Pages.Orders
{
    public class AddItemModel : PageModel
    {
        private readonly ExcellentTaste.Data.ExcellentTasteContext _context;

        public AddItemModel(ExcellentTaste.Data.ExcellentTasteContext context)
        {
            _context = context;
        }
        public PartialViewResult OnGet(int orderId, int productId)
        {
            var order = _context.Order
                .Include(o => o.Items)
                .ThenInclude(item => item.Product)
                .FirstOrDefault(o => o.Id == orderId);
            var product = _context.Product.Find(productId);
            var newItem = new OrderItem()
            {
                IsReady = false,
                Order = order,
                Product = product,
                Quantity = 1
            };
            order.Items.Add(newItem);
            _context.Attach(order).State = EntityState.Modified;
            _context.SaveChanges();
            var model = order.Items;
            return Partial("OrderItems/_ItemList", model);
        }
    }
}
