using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Repositories;

namespace ParkNet_Cristovao.Machado.Pages.TariffTicketGestor
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext _context;
        private readonly TariffTicketRepository _tariffTicketRepository;

        public CreateModel(ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext context,
            TariffTicketRepository tariffticketRepository)
        {
            _tariffTicketRepository = tariffticketRepository;
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TariffTicket TariffTicket { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

           await _tariffTicketRepository.AddTariffTicket(TariffTicket);

            return RedirectToPage("./Index");
        }
    }
}
