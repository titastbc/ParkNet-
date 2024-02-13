using ParkNet_Cristovao.Machado.Data.Entities;
using ParkNet_Cristovao.Machado.Data.Repositories;

namespace ParkNet_Cristovao.Machado.Data.Services
{
    public class WalletManager
    {
        private readonly CustomerRepository _customerRepository;
        public WalletManager(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        
    }
}
