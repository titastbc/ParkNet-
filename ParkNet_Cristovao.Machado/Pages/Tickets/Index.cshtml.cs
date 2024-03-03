using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Models;
using ParkNet_Cristovao.Machado.Data.Repositories;

namespace ParkNet_Cristovao.Machado.Pages.Tickets
{
    public class IndexModel : PageModel
    {
        private readonly ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext _context;
        private readonly VehicleRepository vehicleRepository;

        public IndexModel(ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext context
            , VehicleRepository vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
            _context = context;
        }

        public IList<TicketRequestModel> TicketRequestModel { get;set; } = default!;
        public IList <Ticket> Tickets { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var vehicles = vehicleRepository.GetVehiclesByUserId(userid).Result;
            foreach (var vehicle in vehicles)
            {
                Tickets = await _context.Ticket.Where(p => p.VehicleId == vehicle.Id).ToListAsync();
            }
            TicketRequestModel = await _context.TicketRequestModel.ToListAsync();
        }
    }
}
