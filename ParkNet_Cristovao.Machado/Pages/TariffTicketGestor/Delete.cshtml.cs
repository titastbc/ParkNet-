using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Repositories;

namespace ParkNet_Cristovao.Machado.Pages.TariffTicketGestor
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext _context;
        private readonly TariffTicketRepository _tariffTicketRepository;

        public DeleteModel(ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext context,
            TariffTicketRepository tariffTicketRepository)
        {
            _tariffTicketRepository = tariffTicketRepository;
            _context = context;
        }

        [BindProperty]
        public TariffTicket TariffTicket { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tariffticket = await _context.TariffTickets.FirstOrDefaultAsync(m => m.Id == id);

            if (tariffticket == null)
            {
                return NotFound();
            }
            else
            {
                TariffTicket = tariffticket;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           await _tariffTicketRepository.DeleteTariffTicket(id);

            return RedirectToPage("./Index");
        }
    }
}
