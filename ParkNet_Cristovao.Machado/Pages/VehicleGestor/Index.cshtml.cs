using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Repositories;

namespace ParkNet_Cristovao.Machado.Pages.VehicleGestor
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext _context;
        private readonly VehicleRepository _vehicleRepository;

        public IndexModel(VehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public List<Vehicle> Vehicle { get;set; } = default!;
        public string UserId { get; set; }
        public async Task OnGetAsync()
        {
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Vehicle =  await _vehicleRepository.GetVehiclesByUserId(UserId);
        }
    }
}
