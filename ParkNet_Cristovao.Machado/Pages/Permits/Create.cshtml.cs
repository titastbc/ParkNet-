using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParkNet_Cristovao.Machado.Data;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Models;
using ParkNet_Cristovao.Machado.Data.Repositories;
using ParkNet_Cristovao.Machado.Data.Services;

namespace ParkNet_Cristovao.Machado.Pages.PermitRequest
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext _context;
        private readonly TariffPermitRepository _tariffPermit;
        private readonly StringHelper stringHelper;
        private readonly WalletManager _walletManager;
        public CreateModel(ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext context, TariffPermitRepository tariffPermit
            , StringHelper stringHelper, WalletManager walletManager)
        {
            _context = context;
            _tariffPermit = tariffPermit;
            this.stringHelper = stringHelper;
            _walletManager = walletManager;
        }

        public IActionResult OnGet()
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            userbalance = _walletManager.GetUserBalance(userid);
            var select = stringHelper.PricesAndPeriodsToString();

            ViewData["Periods"] = new SelectList(select);
            ViewData["Vehicles"] = new SelectList(_context.Vehicle.Where(v => v.UserId == userid), "Id", "Plate");
            ViewData["Parks"] = new SelectList(_context.Park.ToList(), "Id", "Name");
            return Page();

        }

        [BindProperty]
        public PermitRequestModel _PermitRequestModel { get; set; } = default!;
        public double userbalance { get; set; }
        public Transactions transactions { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _PermitRequestModel.Period = _PermitRequestModel.Period[0].ToString();
            _PermitRequestModel.Userid = userid;
            _context.PermitRequestModel.Add(_PermitRequestModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./FinalDetails");
        }
    }
}
