using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParkNet_Cristovao.Machado.Data.Entities;

namespace ParkNet_Cristovao.Machado.Pages.TariffTicketGestor
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext _context;

        public IndexModel(ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<TariffTicket> TariffTicket { get;set; } = default!;

        public IList<DailyTicketTariff> DailyTicket { get;set; } = default!;

        public async Task OnGetAsync()
        {
            TariffTicket = await _context.TariffTickets.ToListAsync();
            DailyTicket = await _context.DailyTicketTariff.ToListAsync();
        }
    }
}
