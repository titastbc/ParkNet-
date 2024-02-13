using Microsoft.EntityFrameworkCore;
using ParkNet_Cristovao.Machado.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkNet_Cristovao.Machado.Data.Repositories
{
    public class ParkRepository
    {
        ApplicationDbContext _context;
        public ParkRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Park> AddPark(Park park, string layout)
        {
            park.Layout = layout;
            _context.Park.Add(park);
            await _context.SaveChangesAsync();
            return park;
            
        }
        public async Task<Park> GetPark(int id)
        {
            return await _context.Park.Where(p => p.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Park> GetParkByIdAsync(int id)
        {
            return await _context.Park.FindAsync(id);
        }
        public async Task<Park> DeletePark(int id)
        {
            var park = await _context.Park.FindAsync(id);
            if (park != null)
            {
            _context.Park.Remove(park);
            await _context.SaveChangesAsync();

            }
            else return null;
            return park;
        }
        public async Task<Park> UpdateParkAsync(Park park)
        {
            _context.Park.Attach(park).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return park;
        }
        public async Task<List<Park>> GetParks()
        {
            return await _context.Park.ToListAsync();
        }
    }
}
