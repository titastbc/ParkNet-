using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Repositories;
using ParkNet_Cristovao.Machado.Data.Services;

namespace ParkNet_Cristovao.Machado.Pages.ParkEditor
{
    public class EditModel : PageModel
    {
        private readonly ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext _context;
        private readonly FloorRepository _FloorRepository;
        private readonly LayoutGestorService _LayoutGestorService;
        private readonly ParkRepository _parkrepository;
        private readonly GeneralRepository _GeneralRepository;
        public EditModel(ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext context, FloorRepository floorRepository
            , ParkRepository parkRepository, LayoutGestorService layoutGestorService, GeneralRepository generalRepository)
        {   
            _GeneralRepository = generalRepository;
            _LayoutGestorService = layoutGestorService;
            _parkrepository = parkRepository;
            _FloorRepository = floorRepository;
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
            Floors = await _FloorRepository.GetFloorsAsync(id.Value);
            if (park == null)
            {
                return NotFound();
            }
            Park = park;
            return Page();
        }
        public List<Floor> Floors { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Floors = _LayoutGestorService.FloorBuilder(Park.Id, Park.Layout);
            var parkingspacenames = _LayoutGestorService.GetNames(Park.Layout.Split("\r\n"));
            var ids = _GeneralRepository._FloorRepository.GetFloorsId(Floors);

            Spaces = _LayoutGestorService.GetPlaces(Park.Layout, ids, parkingspacenames);

            await _GeneralRepository.UpdateMultiEntities(Park, Park.Layout, Floors, Spaces);


            return RedirectToPage("./Index");
        }

    }
}
