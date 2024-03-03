using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Migrations;
using ParkNet_Cristovao.Machado.Data.Services;

namespace ParkNet_Cristovao.Machado.Pages.Permits
{
    [Authorize]
    public class FinalDetailsModel : PageModel
    {
        private readonly ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext _context;
        private readonly LayoutGestorService _layoutGestorService;
        private readonly Checker _checker;
        public readonly WalletManager _walletManager;

        public FinalDetailsModel(ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext context
            , LayoutGestorService layoutGestorService,
            Checker checker, WalletManager walletManager)
        {
            _walletManager = walletManager;
            _layoutGestorService = layoutGestorService;
            _context = context;
            _checker = checker;
        }

        public IActionResult OnGet()
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            userbalance = _walletManager.GetUserBalance(userid);
            var data = _context.PermitRequestModel.Where(p => p.Userid == userid).OrderBy(p => p.Id).LastOrDefault();
            Periodid = data.Period;
            Price = _context.TariffPermits.Where(t => t.Id == int.Parse(Periodid))
                .Select(p => p.Price).FirstOrDefault();
            Vehicle vehicle = _context.Vehicle.Find(data.VehicleId);
            type = vehicle.Type;
            Layout = _layoutGestorService.LayouFromBdWithFreeSpaces(data.Parkid, vehicle);
            List<ParkingSpace> Freespaces = _checker.FreePlacesChekcer(_context.Floor.Where(f => f.ParkId == data.Parkid).ToList(), vehicle);
            ViewData["ParkingSpace"] = new SelectList(Freespaces, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Permit Permit { get; set; } = default!;
        public List<ParkingSpace> ParkingSpaces { get; set; } = default!;
        public string[,] Layout { get; set; } = default!;
        public decimal Price { get; set; }
        public string Periodid { get; set; }
        public double userbalance { get; set; }
        public string type { get; set; }
        public Transactions transactions { get; set; } = default!;
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var data = _context.PermitRequestModel.Where(p => p.Userid == userid).OrderBy(p => p.Id).LastOrDefault();
            Periodid = data.Period;

            var period = _context.TariffPermits.Where(t => t.Id == int.Parse(Periodid))
                .Select(p => p.Period).FirstOrDefault();
            Permit permit = new Permit();
            {
                permit.StartDate = DateTime.Now;
                permit.EndDate = DateTime.Now.AddMonths(period);
                permit.VehicleId = _context.PermitRequestModel.Where(p => p.Userid == userid)
                    .OrderBy(p => p.Id)
                    .Select(p => p.VehicleId)
                    .LastOrDefault();
                permit.ParkingSpaceId = Permit.ParkingSpaceId;
            }
            Price = _context.TariffPermits.Where(t => t.Id == int.Parse(Periodid))
    .Select(p => p.Price).FirstOrDefault();
            Transactions transactions = new Transactions();
            {
                transactions.Id = Guid.NewGuid().ToString();
                transactions.CustomerId = userid;
                transactions.Description = "Permit";
                transactions.Date = DateTime.Now;
                transactions.Value = (double)Price * -1;
            }
            _context.Transactions.Add(transactions);
            _context.Permit.Add(permit);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
