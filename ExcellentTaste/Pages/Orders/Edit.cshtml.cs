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

        [BindProperty]
        public IEnumerable<OrderItem> OrderItems { get; set; }

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

            var formdata = Request.Form;
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
            //TODO: Refactor to match the new data flow.
            var selectedCategory = _context.Categories
                .Include(cat => cat.Products)
                .ThenInclude(pc => pc.Product)
                .ThenInclude(p => p.ProductTags)
                .ThenInclude(pt => pt.DietTag)
                .FirstOrDefault(cat => cat.Id == categoryId);

            var order = _context.Order.FirstOrDefault(order => order.Id == orderId);

            //fill the items with the items of the order.
            List<OrderItem> items = _context.OrderItem.Where(orderitem => orderitem.Order == order).ToList();

            if (selectedCategory == null)
            {
                throw new NullReferenceException();
            }
            foreach (var product in selectedCategory.Products.Select(p => p.Product))
            {
                //If this product is not yet in the list of products (it hasn't previously been added) add it
                if (!items.Any(i => i.Product == product))
                {
                    items.Add(new OrderItem
                    {
                        Id = -1,
                        Product = product,
                        Order = order,
                        IsReady = false,
                        Quantity = 0
                    });
                }
            }


            return Partial("OrderItems/_ItemList", items);
        }

        public PartialViewResult OnGetCategory(int categoryId)
        {
            List<Product> model = _context.ProductCategories.Where(pc => pc.CategoryId == categoryId).Select(pc => pc.Product).ToList();
            return Partial("OrderItems/_AddItemList", model);
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
            return Partial("OrderItems/_ItemList", order.Items);
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
