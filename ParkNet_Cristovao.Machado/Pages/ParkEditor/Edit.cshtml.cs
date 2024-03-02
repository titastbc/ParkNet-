using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Repositories;
using ParkNet_Cristovao.Machado.Data.Services;

namespace ParkNet_Cristovao.Machado.Pages.ParkEditor
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext _context;
        private readonly FloorRepository _FloorRepository;
        private readonly LayoutGestorService _LayoutGestorService;
        private readonly ParkRepository _parkrepository;
        private readonly GeneralRepository _GeneralRepository;
        public EditModel(ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext context , ParkRepository parkRepository)
        {   
            _parkrepository = parkRepository;
            _context = context;
        }

        [BindProperty]
        public Park Park { get; set; } = default!;
        public List<ParkingSpace> Spaces { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var park =  await _parkrepository.GetParkByIdAsync(id.Value);
            if (park == null)
            {
                return NotFound();
            }
            Park = park;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _parkrepository.UpdateParkAsync(Park);


            return RedirectToPage("./Index");
        }

    }
}
