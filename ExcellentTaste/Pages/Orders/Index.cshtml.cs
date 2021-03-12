using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExcellentTaste.Data;
using ExcellentTaste.Models;

namespace ExcellentTaste.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly ExcellentTaste.Data.ExcellentTasteContext _context;

        public IndexModel(ExcellentTaste.Data.ExcellentTasteContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; }

        public async Task OnGetAsync()
        {
            Order = await _context.Order.ToListAsync();
        }
    }
}
