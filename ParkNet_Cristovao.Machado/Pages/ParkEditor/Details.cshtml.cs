using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Repositories;
using ParkNet_Cristovao.Machado.Data.Services;

namespace ParkNet_Cristovao.Machado.Pages.ParkEditor
{
    public class DetailsModel : PageModel
    {
        private readonly ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext _context;
        private readonly LayoutGestorService _layoutGestorService;
        private readonly FloorRepository _floorRepository;
        public DetailsModel(ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext context, LayoutGestorService layoutGestorService
            , FloorRepository floorRepository)
        {
            _floorRepository = floorRepository;
            _layoutGestorService = layoutGestorService;
            _context = context;
        }

        public Park Park { get; set; } = default!;
        public string[,] Layout { get; set; } = default!;
        public List<Floor> Floors { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var park = await _context.Park.FirstOrDefaultAsync(m => m.Id == id);
            var names = _layoutGestorService.GetNames(park.Layout.Split("\n"));
            var ids =  _floorRepository.GetFloorIdByParkId(park.Id);
            Floors = _layoutGestorService.FloorBuilder(park.Id, park.Layout);
            Layout = _layoutGestorService.LayouFromBd(park.Id);

            if (park == null)
            {
                return NotFound();
            }
            else
            {
                Park = park;
            }
            return Page();
        }
    }
}
