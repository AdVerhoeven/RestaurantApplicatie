using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExcellentTaste.Data;
using ExcellentTaste.Models;

namespace ExcellentTaste.Pages.DietTags
{
    public class DeleteModel : PageModel
    {
        private readonly ExcellentTaste.Data.ExcellentTasteContext _context;

        public DeleteModel(ExcellentTaste.Data.ExcellentTasteContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DietTag DietTag { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DietTag = await _context.DietTag.FirstOrDefaultAsync(m => m.Id == id);

            if (DietTag == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DietTag = await _context.DietTag.FindAsync(id);

            if (DietTag != null)
            {
                _context.DietTag.Remove(DietTag);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
