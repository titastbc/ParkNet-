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
        private readonly ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext _Context;
        private readonly FloorRepository _FloorRepository;
        private readonly LayoutGestorService _LayoutGestorService;
        private readonly ParkingSpaceRepository _ParkingSpaceRepository;
        private readonly ParkRepository _parkrepository;
        private readonly GeneralRepository _GeneralRepository;

        public CreateModel(ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext context, LayoutGestorService layoutGestorService,
             GeneralRepository generalRepository)
        {
            _GeneralRepository = generalRepository;
            _Context = context;
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
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            await _GeneralRepository._parkrepository.AddPark(Park, Layout);
            Floors = _LayoutGestorService.FloorBuilder(Park.Id, Layout);
            await _GeneralRepository._FloorRepository.AddMultiFloorAsync(Floors);
            var ids = _GeneralRepository._FloorRepository.GetFloorIdByParkId(Park.Id);
            var ParkingSpacesnames = _LayoutGestorService.GetNames(Layout.Split("\r\n"));
            ParkingSpaces = _LayoutGestorService.GetPlaces(Layout, ids, ParkingSpacesnames);
            _GeneralRepository._ParkingSpaceRepository.AddParkingSpaces(ParkingSpaces);
            return RedirectToPage("./Index");
        }
    }
}
