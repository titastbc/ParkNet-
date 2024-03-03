using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Services;

namespace ParkNet_Cristovao.Machado.Pages.Deposit
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext _context;
        private readonly WalletManager WalletManager;

        public IndexModel(ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext context,
            WalletManager walletManager)
        {
            _context = context;
            WalletManager = walletManager;
        }

        public IList<Transactions> Transactions { get;set; } = default!;
        public double userbalance { get; set; }

        public async Task OnGetAsync()
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            userbalance = WalletManager.GetUserBalance(userid);
            Transactions = await _context.Transactions.Where(p => p.CustomerId == userid)
                .Include(t => t.Customer).ToListAsync();
        }
    }
}
