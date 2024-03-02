using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Repositories;
using ParkNet_Cristovao.Machado.Data.Services;
using System.Collections;
using System.Collections.Generic;

namespace ParkNet_Cristovao.Machado.Pages.Stats
{
    public class IndexModel : PageModel
    {
        public Customer Customer { get; set; }
        public CustomerRepository CustomerRepository { get; set; }
        public IList<Customer> Customers = default;
            public WalletManager WalletManager { get; set; }
        public IndexModel(CustomerRepository customerRepository, WalletManager walletManager)
        {
            WalletManager = walletManager;
            CustomerRepository = customerRepository;
        }
        public void OnGet()
        {
            Customers = CustomerRepository.GetCustomers();

        }
    }
}
