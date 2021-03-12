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

namespace ExcellentTaste.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly ExcellentTaste.Data.ExcellentTasteContext _context;

        public EditModel(ExcellentTaste.Data.ExcellentTasteContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }
        public List<SelectListItem> Options { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Product
                .Include(p => p.ProductTags)
                .ThenInclude(pt => pt.DietTag)
                .FirstOrDefaultAsync(m => m.Id == id);

            Options = _context.DietTag.Select(a =>
                new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Name                    
                }).ToList();

            foreach (var listItem in Options)
            {
                if(Product.ProductTags.Any(tag => tag.DietTagId.ToString() == listItem.Value))
                {
                    listItem.Selected = true;
                }
            }

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int[] dietselection)
        {
            //CHANGE: Removed Request.Form and replaced with parameter in handler as seen above. (also parses string values to ints)
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //Make sure the many-to-many is loaded into the model.
            Product.ProductTags = await _context.ProductTags.Where(pt => pt.ProductId == Product.Id).ToListAsync();
            _context.Attach(Product).State = EntityState.Modified;

            //var dietTagsIds = Request.Form["dietselection"];

            var dietTags = _context.DietTag.Where(tag => dietselection.Any(id => id == tag.Id));
            var newTags = new List<ProductTag>();
            foreach (var dietTag in dietTags)
            {
                var productTag = new ProductTag
                {
                    Product = Product,
                    ProductId = Product.Id,
                    DietTag = dietTag,
                    DietTagId = dietTag.Id
                };
                newTags.Add(productTag);
            }
            //Update many-to-many trough the product.
            //This way there is no need to check if a productTag already exists.
            Product.ProductTags = newTags;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.Id))
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

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
