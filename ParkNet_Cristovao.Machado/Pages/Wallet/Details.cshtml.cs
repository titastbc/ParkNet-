using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParkNet_Cristovao.Machado.Data.Entities;

namespace ParkNet_Cristovao.Machado.Pages.Deposit
{
    public class DetailsModel : PageModel
    {
        private readonly ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext _context;

        public DetailsModel(ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext context)
        {
            _context = context;
        }

        public Transactions Transactions { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactions = await _context.Transactions.FirstOrDefaultAsync(m => m.Id == id);
            if (transactions == null)
            {
                return NotFound();
            }
            else
            {
                Transactions = transactions;
            }
            return Page();
        }
    }
}
