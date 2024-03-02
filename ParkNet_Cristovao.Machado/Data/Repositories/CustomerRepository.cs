using ParkNet_Cristovao.Machado.Data.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Collections.Generic;


namespace ParkNet_Cristovao.Machado.Data.Repositories
{
    public class CustomerRepository
    {
       public ApplicationDbContext _context;
        private readonly UserManager<Customer> _userManager;
        public CustomerRepository(ApplicationDbContext context, UserManager<Customer> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<Customer> AddCustomer(Customer customer)
        {
            await _userManager.CreateAsync(customer, customer.PasswordHash);
            await _context.SaveChangesAsync();
            return customer;
        }
        public async Task<Customer> GetCustomer(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }
        public async Task<Customer> DeleteCustomer(string id)
        {
            var customer = await _userManager.FindByIdAsync(id);
            if (customer != null)
            {
               await _userManager.DeleteAsync(customer);
                await _context.SaveChangesAsync();
            }
            else return null;
            return customer;
        }
        public async Task<Customer> UpdateCustomer(Customer customer)
        {
          await  _userManager.UpdateAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
        public string GetCustomerPaymentMethod(string id)
        {
            var customer = _context.Users.Where(c => c.Id == id).FirstOrDefault();
            var method = customer.BankCardNumber;
            return method;
        }
        public async Task<Customer> GetCustomerByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public List<Customer> GetCustomers()
        {
            return _context.Users.ToList();
        }

    }
}
