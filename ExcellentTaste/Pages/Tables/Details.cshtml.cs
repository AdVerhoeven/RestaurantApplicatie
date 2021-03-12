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
    public class DetailsModel : PageModel
    {
        private readonly ExcellentTaste.Data.ExcellentTasteContext _context;

        public DetailsModel(ExcellentTaste.Data.ExcellentTasteContext context)
        {
            _context = context;
        }

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
    }
}
