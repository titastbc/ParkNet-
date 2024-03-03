using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Models;

namespace ParkNet_Cristovao.Machado.Pages.PermitRequest
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext _context;

        public DeleteModel(ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PermitRequestModel PermitRequestModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permitrequestmodel = await _context.PermitRequestModel.FirstOrDefaultAsync(m => m.Id == id);

            if (permitrequestmodel == null)
            {
                return NotFound();
            }
            else
            {
                PermitRequestModel = permitrequestmodel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permitrequestmodel = await _context.PermitRequestModel.FindAsync(id);
            if (permitrequestmodel != null)
            {
                PermitRequestModel = permitrequestmodel;
                _context.PermitRequestModel.Remove(PermitRequestModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
