using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Models;
using ParkNet_Cristovao.Machado.Data.Services;

namespace ParkNet_Cristovao.Machado.Pages.Tickets
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext _context;
        private readonly WalletManager _walletManager;
        private readonly Checker checker;

        public CreateModel(ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext context,
            WalletManager walletManager, Checker checker)
        {
            this.checker = checker;
            _walletManager = walletManager;
            _context = context;
        }

        public IActionResult OnGet()
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            HasTicket = checker.ActiveTicketCheker(userid);
            Balance = _walletManager.GetUserBalance(userid);
            string[] values = { "Daily", "Normal"};
            ViewData["Values"] = new SelectList(values.ToList());
            ViewData["Vehicles"] = new SelectList(_context.Vehicle.Where(v => v.UserId == userid), "Id", "Plate");
            ViewData["Parks"] = new SelectList(_context.Park.ToList(), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public TicketRequestModel TicketRequestModel { get; set; } = default!;
        [BindProperty]
        public string types { get; set; }
        public double Balance { get; set; }

        public bool HasTicket { get; set; }

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
            if(types == "Daily")
            {
                TicketRequestModel.IsDaily = true;
            }
            else
            {
                TicketRequestModel.IsDaily = false;
            }
            _context.TicketRequestModel.Add(TicketRequestModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./FinalDetails");
        }
    }
}
