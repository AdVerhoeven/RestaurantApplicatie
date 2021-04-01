using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExcellentTaste.Data;
using ExcellentTaste.Models;

namespace ExcellentTaste.Pages.Orders
{
    public class EditModel : PageModel
    {
        private readonly ExcellentTaste.Data.ExcellentTasteContext _context;

        public EditModel(ExcellentTaste.Data.ExcellentTasteContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order Order { get; set; }

        public List<CategoryViewModel> Categories { get; set; }

        public int[] ProductIds { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _context.Order
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Product)
                .Include(o => o.OrderTables)
                .ThenInclude(ot => ot.Table)
                .FirstOrDefaultAsync(m => m.Id == id);

            Categories = new List<CategoryViewModel>();
            var catList = await _context.Categories
                .Include(cat => cat.Products)
                .ThenInclude(pl => pl.Product)
                .ToListAsync();

            foreach (var cat in catList)
            {
                Categories.Add(new CategoryViewModel(cat));
            }

            if (Order == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(List<OrderItem> orderItems)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Order.Items = await _context.OrderItem.Where(item => item.OrderId == Order.Id).ToListAsync();

            _context.Attach(Order).State = EntityState.Modified;

            Order.Items = orderItems;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(Order.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        public PartialViewResult OnGetCategory(int categoryId)
        {
            List<Product> model = _context.ProductCategories.Where(pc => pc.CategoryId == categoryId).Select(pc => pc.Product).ToList();
            return Partial("OrderItems/_AddItemList", model);
        }

        public PartialViewResult OnGetRemoveItem(int orderId, int itemId)
        {
            var order = _context.Order
                .Include(o => o.Items)
                .ThenInclude(item => item.Product)
                .FirstOrDefault(o => o.Id == orderId);

            var item = _context.OrderItem.FirstOrDefault(it => it.Id == itemId);
            order.Items.Remove(item);

            _context.Attach(item).State = EntityState.Deleted;
            _context.Attach(order).State = EntityState.Modified;
            _context.SaveChanges();

            var model = order.Items;
            return Partial("OrderItems/_ItemList", model);
        }

        public PartialViewResult OnGetAddItem(int orderId, int productId)
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

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
