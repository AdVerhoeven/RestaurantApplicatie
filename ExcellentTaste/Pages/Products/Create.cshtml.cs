using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExcellentTaste.Data;
using ExcellentTaste.Models;

namespace ExcellentTaste.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly ExcellentTaste.Data.ExcellentTasteContext _context;

        public CreateModel(ExcellentTaste.Data.ExcellentTasteContext context)
        {
            _context = context;
        }

        public List<SelectListItem> Options { get; set; }
        public IActionResult OnGet()
        {
            Options = _context.DietTag.Select(a =>
            new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }
            ).ToList();
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Product.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
