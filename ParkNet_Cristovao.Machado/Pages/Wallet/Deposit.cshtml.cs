using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Models;
using ParkNet_Cristovao.Machado.Data.Repositories;

namespace ParkNet_Cristovao.Machado.Pages.Deposit
{
    public class CreateModel : PageModel
    {
        private readonly ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext _context;
        private readonly CustomerRepository _customerRepository;

        public CreateModel(ParkNet_Cristovao.Machado.Data.Entities.ApplicationDbContext context, CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            _context = context;
        }

        public IActionResult OnGet()
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["PaymentMethod"] = new SelectList(_context.Users.Where(p => p.Id == userid), "Id", "BankCardNumber");
            return Page();
        }

        [BindProperty]
        public Transactions Transaction { get; set; } = default!;
        public DepositModel Deposit { get; set; } = default!;
        [BindProperty]
        public double value { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Transactions transaction = new Transactions();
            {
                transaction.Id = Guid.NewGuid().ToString();
                transaction.CustomerId = userid;
                transaction.Date = DateTime.Now;
                transaction.Description = "Deposit";
                transaction.Value = value;
            }


            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
