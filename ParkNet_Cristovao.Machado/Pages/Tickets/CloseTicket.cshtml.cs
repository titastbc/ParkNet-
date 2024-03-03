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
using ParkNet_Cristovao.Machado.Data.Repositories;
using ParkNet_Cristovao.Machado.Data.Services;

namespace ParkNet_Cristovao.Machado.Pages.Tickets
{
    [Authorize]
    public class CloseTicketModel : PageModel
    {
        private readonly ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext _context;
        private readonly VehicleRepository vehicleRepository;
        private readonly WalletManager _walletManager;
        private readonly PriceCalculator _PriceCalculator;
        private readonly TicketRequestModel ticketRequestModel;

        public CloseTicketModel(ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext context
            , VehicleRepository vehicleRepository, WalletManager walletManager, PriceCalculator priceCalculator
            , TicketRequestModel ticketRequestModel)
        {
            this.ticketRequestModel = ticketRequestModel;
            _walletManager = walletManager;
            this.vehicleRepository = vehicleRepository;
            _context = context;
            _PriceCalculator = priceCalculator;
        }

        public IActionResult OnGet()
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var vehicles = vehicleRepository.GetVehiclesByUserId(userid).Result;
            userbalance = _walletManager.GetUserBalance(userid);
            foreach (var vehicle in vehicles)
            {
                Ticket = _context.Ticket
                    .Where(p => p.VehicleId == vehicle.Id).OrderBy(v => v.Id).LastOrDefault();
            }
            if (Ticket.IsDaily)
            {
                Price = _PriceCalculator.CalculateDailyPrice(userid);
            }
            else 
            Price =  _PriceCalculator.CalculatePrice(Ticket.StartDate).Result;
            return Page();
        }

        [BindProperty]
        public Transactions Transactions { get; set; } = default!;
        public double userbalance { get; set; }
        public decimal Price { get; set; }
        public Ticket Ticket { get; set; } = default!;
        public bool IsDaily { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var vehicles = vehicleRepository.GetVehiclesByUserId(userid).Result;
            foreach (var vehicle in vehicles)
            {
                Ticket = _context.Ticket
                    .Where(p => p.VehicleId == vehicle.Id).OrderBy(v => v.Id).LastOrDefault();
            }
            if (Ticket.IsDaily)
            {
                Price = _PriceCalculator.CalculateDailyPrice(userid);
            }
            else
            Price = _PriceCalculator.CalculatePrice(Ticket.StartDate).Result;

            Transactions = new Transactions();
            Transactions.Id = Guid.NewGuid().ToString();
            Transactions.CustomerId = userid;
            Transactions.Value = (double)Price * -1;
            Transactions.Date = DateTime.Now;
            Transactions.Description = "Ticket";
            if(Ticket.IsDaily)
            {
                Ticket.EndDate = DateTime.Now.AddDays(1);
            }
            else
            {
                Ticket.EndDate = DateTime.Now;
            }

            _context.Ticket.Attach(Ticket).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.Transactions.Add(Transactions);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
