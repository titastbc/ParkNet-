using ParkNet_Cristovao.Machado.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkNet_Cristovao.Machado.Data.Repositories
{
    public class TariffPermitRepository
    {
        public ApplicationDbContext _context { get; set; }
        public TariffPermitRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TariffPermit> Addasync(TariffPermit tariffPermit)
        {
           await _context.TariffPermits.AddAsync(tariffPermit);
            _context.SaveChanges();
            return tariffPermit;
        }

        public void Update(TariffPermit tariffPermit)
        {
            _context.TariffPermits.Update(tariffPermit);
            _context.SaveChanges();
        }

        public void Delete(TariffPermit tariffPermit)
        {
            _context.TariffPermits.Remove(tariffPermit);
            _context.SaveChanges();
        }

        public async Task<TariffPermit> GetById(int id)
        {
            return await _context.TariffPermits.FindAsync(id);
        }

        public List<TariffPermit> GetAll()
        {
            return _context.TariffPermits.ToList();
        }
    }
}
