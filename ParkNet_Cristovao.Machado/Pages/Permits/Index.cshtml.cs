using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public IList<PermitRequestModel> PermitRequestModel { get;set; } = default!;
        public List<Permit> permits { get; set; } = default!;
        public async Task OnGetAsync()
        {
            permits = _context.Permit.ToList();
            PermitRequestModel = await _context.PermitRequestModel.ToListAsync();
        }
    }
}
