using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExcellentTaste.Data;
using ExcellentTaste.Models;

namespace ExcellentTaste.Pages.Tables
{
    public class DeleteModel : PageModel
    {
        private readonly ExcellentTaste.Data.ExcellentTasteContext _context;

        public DeleteModel(ExcellentTaste.Data.ExcellentTasteContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Table Table { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Table = await _context.Table.FirstOrDefaultAsync(m => m.Id == id);

            if (Table == null)
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

            Table = await _context.Table.FindAsync(id);

            if (Table != null)
            {
                _context.Table.Remove(Table);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
