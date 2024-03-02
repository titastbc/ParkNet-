using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Repositories;

namespace ParkNet_Cristovao.Machado.Pages.ParkEditor
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext _context;
        private readonly ParkRepository _parkRepository;

        public DeleteModel(ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext context, ParkRepository parkRepository)
        {
            _parkRepository = parkRepository;
            _context = context;
        }

        [BindProperty]
        public Park Park { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var park = await _context.Park.FirstOrDefaultAsync(m => m.Id == id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

         if(  await _parkRepository.DeletePark(id.Value) == null)
                return NotFound();

            return RedirectToPage("./Index");
        }
    }
}
