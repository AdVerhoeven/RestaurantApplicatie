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

        public List<Product> OrderedProducts { get; set; }

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
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Order).State = EntityState.Modified;

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

        public PartialViewResult OnGetNewItems(int orderId, int categoryId)
        {
            var items = new List<OrderItem>();
            var selected = _context.Categories
                .Include(cat => cat.Products)
                .ThenInclude(pc => pc.Product)
                .ThenInclude(p => p.ProductTags)
                .ThenInclude(pt => pt.DietTag)
                .FirstOrDefault(cat => cat.Id == categoryId);
            var order = _context.Order.FirstOrDefault(order => order.Id == orderId);
            if (selected == null)
            {
                throw new NullReferenceException();
            }
            foreach (var item in selected.Products.Select(p => p.Product))
            {
                items.Add(new OrderItem
                { 
                    Product = item,
                    Order = order,
                    IsReady = false,
                    Quantity = 0
                });
            }

            return Partial("OrderItems/_ItemList", items);
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
