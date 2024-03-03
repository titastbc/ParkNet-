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
    public class FinalDetailsModel : PageModel
    {
        private readonly ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext _context;
        private readonly WalletManager _walletManager;
        private readonly LayoutGestorService _layoutGestorService;
        private readonly Checker _checker;
        private readonly PriceCalculator _PriceCalculator;

        public FinalDetailsModel(ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext context,
            WalletManager walletManager, LayoutGestorService layoutGestorService, Checker checker,
            PriceCalculator priceCalculator
            )
        {
            _PriceCalculator = priceCalculator;
            _context = context;
            _walletManager = walletManager;
            _layoutGestorService = layoutGestorService;
            _checker = checker;
        }

        public IActionResult OnGet()
        {

            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _TicketRequestModel = _context.TicketRequestModel
                .Where(p => p.Userid == userid).OrderBy(p => p.Id).LastOrDefault();
            Vehicle vehicle = _context.Vehicle
                .Find(_TicketRequestModel.VehicleId);
            userbalance = _walletManager.GetUserBalance(userid);
            if (_TicketRequestModel.IsDaily == true)
            {
                Price = _PriceCalculator.CalculateDailyPrice(userid);
            }
            Layout = _layoutGestorService.LayouFromBdWithFreeSpaces(_TicketRequestModel.Parkid, vehicle);
            List<ParkingSpace> Freespaces = _checker.FreePlacesChekcer(_context.Floor
                .Where(f => f.ParkId == _TicketRequestModel.Parkid).ToList(), vehicle);
            ViewData["ParkingSpace"] = new SelectList(Freespaces, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public TicketRequestModel _TicketRequestModel { get; set; } = default!;
        public string[,] Layout { get; set; } = default!;
        public decimal Price { get; set; }
        public double userbalance { get; set; }
        public Transactions transactions { get; set; } = default!;

        [BindProperty]
        public Ticket Ticket { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _TicketRequestModel = _context.TicketRequestModel
    .Where(p => p.Userid == userid).OrderBy(p => p.Id).LastOrDefault();
            Vehicle vehicle = _context.Vehicle
                .Find(_TicketRequestModel.VehicleId);
            try
            {
                Ticket ticket = new Ticket();
                if (_TicketRequestModel.IsDaily == true)
                {
                    ticket = new Ticket
                    {
                        Id = Guid.NewGuid().ToString(),
                        VehicleId = _TicketRequestModel.VehicleId,
                        ParkingSpaceId = Ticket.ParkingSpaceId,
                        StartDate = _TicketRequestModel.StartDate,
                        IsDaily = true
                    };
                }
                else
                {

                    ticket = new Ticket()
                    {
                        Id = Guid.NewGuid().ToString(),
                        VehicleId = _TicketRequestModel.VehicleId,
                        ParkingSpaceId = Ticket.ParkingSpaceId,
                        StartDate = _TicketRequestModel.StartDate,
                        IsDaily = false
                    };
                }

                _context.Ticket.Add(ticket);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir o ticket: {ex.Message}");
                // Trate a exceção conforme necessário
            }
            if (_TicketRequestModel.IsDaily == true)
            {
                return RedirectToPage("./CloseTicket");
            }
            return RedirectToPage("./Index");
        }
    }
}
