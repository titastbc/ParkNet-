using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Repositories;
using System.Linq;

namespace ParkNet_Cristovao.Machado.Data.Services
{
    public class WalletManager
    {
        private readonly CustomerRepository _customerRepository;
        public WalletManager(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        

      public double GetUserBalance(string userid)
        {
           var usertransanction = _customerRepository._context.Transactions.Where(x => x.CustomerId == userid);
            double balance = 0;
            foreach (var transaction in usertransanction )
            {   
                balance += transaction.Value;
            }
            return balance;
        }
    }
}

