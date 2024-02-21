using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParkNet_Cristovao.Machado.Data.Entities;

namespace ParkNet_Cristovao.Machado.Pages.Deposit
{
    public class IndexModel : PageModel
    {
        private readonly ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext _context;

        public IndexModel(ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Transactions> Transactions { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Transactions = await _context.Transactions.Where(p => p.CustomerId == userid)
                .Include(t => t.Customer).ToListAsync();
        }
    }
}
