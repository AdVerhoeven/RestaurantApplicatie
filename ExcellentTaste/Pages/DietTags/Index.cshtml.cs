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
    public class IndexModel : PageModel
    {
        private readonly ExcellentTaste.Data.ExcellentTasteContext _context;

        public IndexModel(ExcellentTaste.Data.ExcellentTasteContext context)
        {
            _context = context;
        }

        public IList<DietTag> DietTag { get;set; }

        public async Task OnGetAsync()
        {
            DietTag = await _context.DietTag.ToListAsync();
        }
    }
}
