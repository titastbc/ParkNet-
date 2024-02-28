using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Models;
using ParkNet_Cristovao.Machado.Data.Services;

namespace ParkNet_Cristovao.Machado.Pages.Tickets
{
    public class CreateModel : PageModel
    {
        private readonly ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext _context;
        private readonly WalletManager _walletManager;

        public CreateModel(ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext context,
            WalletManager walletManager)
        {
            _walletManager = walletManager;
            _context = context;
        }

        public IActionResult OnGet()
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Balance = _walletManager.GetUserBalance(userid);
            ViewData["Vehicles"] = new SelectList(_context.Vehicle.Where(v => v.UserId == userid), "Id", "Plate");
            ViewData["Parks"] = new SelectList(_context.Park.ToList(), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public TicketRequestModel TicketRequestModel { get; set; } = default!;
        public double Balance { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            TicketRequestModel.Userid = userid;
            TicketRequestModel.StartDate = DateTime.Now;

            _context.TicketRequestModel.Add(TicketRequestModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./FinalDetails");
        }
    }
}
