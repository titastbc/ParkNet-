using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Repositories;
using ParkNet_Cristovao.Machado.Data.Services;

namespace ParkNet_Cristovao.Machado.Pages.ParkEditor
{
    public class CreateModel : PageModel
    {
        private readonly ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext _context;
        private readonly FloorRepository _FloorRepository;
        private readonly LayoutGestorService _LayoutGestorService;

        public CreateModel(ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext context, FloorRepository floorRepository, LayoutGestorService layoutGestorService)
        {
            _FloorRepository = floorRepository;
            _context = context;
            _LayoutGestorService = layoutGestorService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Park Park { get; set; } = default!;
        [BindProperty]
        public string Layout { get; set; } = default!;
        public List<Floor> Floors { get; set; } = default!;
        public List<ParkingSpace> ParkingSpaces { get; set; } = default!;
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Park.Add(Park);
            await _context.SaveChangesAsync();
            Floors = _LayoutGestorService.FloorBuilder(Park.Id, Layout);

            await _FloorRepository.AddMultiFloor(Floors);

            return RedirectToPage("./Index");
        }
    }
}
