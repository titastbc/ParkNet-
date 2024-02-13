
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Repositories;

namespace ParkNet_Cristovao.Machado.Pages.VehicleGestor
{
    public class DeleteModel : PageModel
    {
        private readonly VehicleRepository _vehicleRepository;

        public DeleteModel(VehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        [BindProperty]
        public Vehicle Vehicle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _vehicleRepository.GetVehicleById(id.Value);

            if (vehicle == null)
            {
                return NotFound();
            }
            else
            {
                Vehicle = vehicle;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           await _vehicleRepository.DeleteVehicleAsync(id.Value);

            return RedirectToPage("./Index");
        }
    }
}
