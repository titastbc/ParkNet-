using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Repositories;

namespace ParkNet_Cristovao.Machado.Pages.VehicleGestor
{
    public class CreateModel : PageModel
    {
        private readonly ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext _context;
        private readonly VehicleRepository _vehicleRepository;

        public CreateModel(ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext context, VehicleRepository vehicleRepository)
        {
            _context = context;
            _vehicleRepository = vehicleRepository;
        }

        public IActionResult OnGet()
        {
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            VehcileTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Choose an option"},
                new SelectListItem { Value = "Car", Text = "Car" },
                new SelectListItem { Value = "Motorcycle", Text = "Motorcycle" }
            };
            return Page();
        }

        [BindProperty]
        public Vehicle Vehicle { get; set; } = default!;
        [Required(ErrorMessage = "Type is Raquired")]
        public List<SelectListItem> VehcileTypes;
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            Vehicle.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (Vehicle.Type == "")
            {
                return RedirectToPage("./CreateCar");
            }
           await _vehicleRepository.AddVehicleAsync(Vehicle);

            return RedirectToPage("./Index");
        }
    }
}
