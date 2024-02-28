using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Models;

namespace ParkNet_Cristovao.Machado.Pages.Tickets
{
    public class DeleteModel : PageModel
    {
        private readonly ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext _context;

        public DeleteModel(ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TicketRequestModel TicketRequestModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketrequestmodel = await _context.TicketRequestModel.FirstOrDefaultAsync(m => m.Id == id);

            if (ticketrequestmodel == null)
            {
                return NotFound();
            }
            else
            {
                TicketRequestModel = ticketrequestmodel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketrequestmodel = await _context.TicketRequestModel.FindAsync(id);
            if (ticketrequestmodel != null)
            {
                TicketRequestModel = ticketrequestmodel;
                _context.TicketRequestModel.Remove(TicketRequestModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
