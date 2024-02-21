using Microsoft.EntityFrameworkCore;
using ParkNet_Cristovao.Machado.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkNet_Cristovao.Machado.Data.Repositories
{
    public class TariffTicketRepository
    {
        ApplicationDbContext _context;
        public TariffTicketRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TariffTicket> GetTariffTicketById(int id)
        {
            return await _context.TariffTickets.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<List<TariffTicket>> GetTariffTickets()
        {
            return await _context.TariffTickets.OrderBy(t => t.Inicialperiod).ToListAsync();
        }
        public async Task AddTariffTicket(TariffTicket tariffTicket)
        {
           await _context.TariffTickets.AddAsync(tariffTicket);
          await _context.SaveChangesAsync();
        }
        public async Task UpdateTariffTicket(TariffTicket tariffTicket)
        {
            _context.Attach(tariffTicket).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteTariffTicket(int? id)
        {
            var tariffTicket = await _context.TariffTickets.FindAsync(id);
            _context.TariffTickets.Remove(tariffTicket);
            await _context.SaveChangesAsync();
        }


    }
}
