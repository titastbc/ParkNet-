using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Models;

namespace ParkNet_Cristovao.Machado.Pages.PermitRequest
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext _context;

        public EditModel(ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext context)
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

            var permitrequestmodel =  await _context.PermitRequestModel.FirstOrDefaultAsync(m => m.Id == id);
            if (permitrequestmodel == null)
            {
                return NotFound();
            }
            PermitRequestModel = permitrequestmodel;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(PermitRequestModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermitRequestModelExists(PermitRequestModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PermitRequestModelExists(int id)
        {
            return _context.PermitRequestModel.Any(e => e.Id == id);
        }
    }
}
