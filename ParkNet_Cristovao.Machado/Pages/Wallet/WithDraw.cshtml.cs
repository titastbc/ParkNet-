using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Services;

namespace ParkNet_Cristovao.Machado.Pages.Wallet
{
    [Authorize]
    public class WithDrawModel : PageModel
    {
        private readonly ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext _context;
        public readonly WalletManager _walletManager;
        public readonly Checker _checker;

        public WithDrawModel(ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext context
            , WalletManager walletManager,
Checker checker)
        {
            _context = context;
            _walletManager = walletManager;
            _checker = checker;
        }

        public IActionResult OnGet()
        {
            ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "Id");
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var vehicles = _context.Vehicle.Where(v => v.UserId == userid);
            foreach (var v in vehicles)
            {
                var ticket = _context.Ticket.
                    Where(t => t.VehicleId == v.Id && t.EndDate == null).
                    OrderBy(p => p.Id).
                    LastOrDefault();
                if (ticket != null)
                {
                    Nullticket = true;
                }
                else
                {
                    Nullticket = false;
                }
            }
            return Page();
        }

        [BindProperty]
        public double Value { get; set; }
        public double Userbalance { get; set; }
        public bool HasTicket { get; set; }
        public bool Nullticket { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userbalance = _walletManager.GetUserBalance(userid);

            if (Value > userbalance)
            {
                ModelState.AddModelError("value", "O valor de retirada não pode ser maior que o saldo disponível.");
                return Page();
            }
            var vehicles = _context.Vehicle.Where(v => v.UserId == userid);
            var ticket1 = new Data.Entities.Ticket();
            foreach (var v in vehicles)
            {
                ticket1 = _context.Ticket.Where(t => t.VehicleId == v.Id && t.EndDate == null).OrderBy(t => t.Id).LastOrDefault();
                if (ticket1 != null)
                {
                    ModelState.AddModelError("value", "Não é possível retirar dinheiro enquanto houver tickets em aberto.");
                    return Page();
                }
            }

            var transaction = new Transactions();
            {
                transaction.Id = Guid.NewGuid().ToString();
                transaction.CustomerId = userid;
                transaction.Date = DateTime.Now;
                transaction.Description = "Withdraw";
                transaction.Value = Value * -1;
            }
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
