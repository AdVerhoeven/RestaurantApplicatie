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
    public class IndexModel : PageModel
    {
        private readonly ExcellentTaste.Data.ExcellentTasteContext _context;

        public IndexModel(ExcellentTaste.Data.ExcellentTasteContext context)
        {
            _context = context;
        }

        public IList<Table> Table { get;set; }

        public async Task OnGetAsync()
        {
            Table = await _context.Table.Where(t => t.OrderTables.Count == 0).ToListAsync();
        }
    }
}
