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


            //TODO: proper binding.
            var formdata = Request.Form;
            var productIds = Request.Form["item.Product.Id"];
            var quantity = Request.Form["item.Quantity"];
            var status = Request.Form["item.isReady"];
            //Needs proper binding and fixing, duplicate entries and more are being added.
            Order.Items = await _context.OrderItem.Where(orit => orit.Order == Order).ToListAsync();
            for (int i = 0; i < productIds.Count; i++)
            {
                int id = int.Parse(productIds[i]);
                if (Order.Items.Any(x => x.Id == id))
                {
                    var updatedItem = Order.Items.FirstOrDefault(x => x.Id == id);
                    if(int.Parse(quantity[i]) == 0)
                    {
                        _context.Remove(updatedItem);
                        continue;
                    }
                    updatedItem.IsReady = bool.Parse(status[i]);
                    updatedItem.Quantity = int.Parse(quantity[i]);
                }
                else
                {
                    if(int.Parse(quantity[i]) == 0)
                    {
                        continue;
                    }
                    Order.Items.Add(new OrderItem
                    {
                        Order = Order,
                        Product = _context.Product.FirstOrDefault(product => product.Id == id),
                        IsReady = bool.Parse(status[i]),
                        Quantity = int.Parse(quantity[i])
                    });
                }
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

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
