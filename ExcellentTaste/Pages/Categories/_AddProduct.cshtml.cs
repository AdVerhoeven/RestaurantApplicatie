using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExcellentTaste.Data;
using ExcellentTaste.Models;

namespace ExcellentTaste.Pages.Categories
{
    public class AddProductsModel : PageModel
    {
        private readonly ExcellentTaste.Data.ExcellentTasteContext _context;

        public AddProductsModel(ExcellentTaste.Data.ExcellentTasteContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get; set; }

        [BindProperty(SupportsGet = true)]
        public string searchString { get; set; }
        public async Task<PartialViewResult> OnGetAsync()
        {
            var products = from p in _context.Product select p;

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString));
            }
            Product = await _context.Product.ToListAsync();

            return Partial("_AddProduct", Product);
        }
    }
}
