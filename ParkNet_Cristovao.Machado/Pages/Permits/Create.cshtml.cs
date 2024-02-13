using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Services;

namespace ParkNet_Cristovao.Machado.Pages.Permits
{
    public class CreateModel : PageModel
    {
        private readonly ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext _context;
        private readonly LayoutGestorService _layoutGestorService;
        private readonly Checker _checker;

        public CreateModel(ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext context,
            LayoutGestorService layoutGestorService,
            Checker checker)
        {
            _layoutGestorService = layoutGestorService;
            _context = context;
            _checker = checker;
        }

        public IActionResult OnGet()
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var parks = _context.Park.ToList();
            var Periods = _context.TariffPermits.ToList();
            ViewData["Parks"] = new SelectList(parks, "Id", "Name");
            ViewData["Vehicles"] = new SelectList(_context.Veihicle.Where(v => v.UserId == userid), "Id" , "Plate");
            ViewData["Periods"] = new SelectList(Periods, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Permit Permit { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Permit.Add(Permit);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
