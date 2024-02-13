using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParkNet_Cristovao.Machado.Data.Entities;

namespace ParkNet_Cristovao.Machado.Pages.Permits
{
    public class IndexModel : PageModel
    {
        private readonly ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext _context;

        public IndexModel(ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Park> Parks { get; set; }
        public IList<Permit> Permit { get; set; } = default!;

        public Park park { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            park = await _context.Park.Where(p => p.Name == park.Name).FirstOrDefaultAsync();
            return RedirectToPage("./Create", new { id = park.Id });
        }

        public async Task OnGetAsync()
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Parks = await _context.Park.ToListAsync();
            Permit = await _context.Permit.Where(p => p.Vehicle.UserId == userid).ToListAsync();
        }
    }
}
