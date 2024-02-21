using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Models;

namespace ParkNet_Cristovao.Machado.Pages.PermitRequest
{
    public class IndexModel : PageModel
    {
        private readonly ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext _context;

        public IndexModel(ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Permit> permits { get; set; } = new List<Permit>();
        public List<PermitShareModel> _PermitShareModel { get; set; } = new List<PermitShareModel>();
        public async Task OnGetAsync()
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var uservehicles = _context.Vehicle.Where(v => v.UserId == userid).ToList();
            foreach (var vehicle in uservehicles)
            {
                var aux = _context.Permit.Where(p => p.VehicleId == vehicle.Id).ToList();
                permits.AddRange(aux);
            }
            foreach (var permit in permits)
            {
                PermitShareModel model = new PermitShareModel
                {
                    ParkingSpaceName = await _context.ParkingSpace.Where(x => x.Id == permit.ParkingSpaceId).Select(p => p.Name).FirstOrDefaultAsync(),
                    Plate = await _context.Vehicle.Where(x => x.Id == permit.VehicleId).Select(p => p.Plate).FirstOrDefaultAsync(),
                    StartDate = permit.StartDate,
                    EndDate = permit.EndDate,
                    PermitId = permit.Id
                };
                _PermitShareModel.Add(model);
            }
        }
    }
}
