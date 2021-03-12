using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExcellentTaste.Data;
using ExcellentTaste.Models;

namespace ExcellentTaste.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly ExcellentTaste.Data.ExcellentTasteContext _context;

        public CreateModel(ExcellentTaste.Data.ExcellentTasteContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            FreeTables = _context.Table.Where(t => t.OrderTables.Count == 0).Select(table => new SelectListItem
            {
                Text = table.Name,
                Value = table.Id.ToString()
            }).ToList();

            return Page();
        }

        [BindProperty]
        public Order Order { get; set; }

        public List<SelectListItem> FreeTables { get; set; }

        [BindProperty]
        public int[] TableIds { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            
            _context.Order.Add(Order);
            await _context.SaveChangesAsync();
            var order = _context.Order.Find(Order);
            var selection = Request.Form["selectedTables"];
            foreach (var item in selection)
            {
                Order.OrderTables.Add(new OrderTable
                {
                    OrderId = order.Id,
                    Table = _context.Table.FirstOrDefault(t => t.Id.ToString() == item),
                    Reservation = DateTime.Now
                });

            }
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
