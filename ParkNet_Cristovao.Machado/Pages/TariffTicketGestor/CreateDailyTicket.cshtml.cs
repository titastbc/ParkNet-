using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParkNet_Cristovao.Machado.Data.Entities;

namespace ParkNet_Cristovao.Machado.Pages.TariffTicketGestor
{
    public class CreateDailyTicketModel : PageModel
    {
        private readonly ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext _context;

        public CreateDailyTicketModel(ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            DailyTicket = _context.DailyTicketTariff.ToList();
            return Page();
        }

        [BindProperty]
        public DailyTicketTariff DailyTicketTariff { get; set; } = default!;
        public IList<DailyTicketTariff> DailyTicket { get;set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.DailyTicketTariff.Add(DailyTicketTariff);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
