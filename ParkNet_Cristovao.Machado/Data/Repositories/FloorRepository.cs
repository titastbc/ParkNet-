using Microsoft.EntityFrameworkCore;
using ParkNet_Cristovao.Machado.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkNet_Cristovao.Machado.Data.Repositories
{
    public class FloorRepository
    {
        public ApplicationDbContext _context;
        public FloorRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Floor AddFloor(Floor floor)
        {
            _context.Floor.Add(floor);
            _context.SaveChanges();
            return floor;
        }
        public async Task<Floor> GetFloorByIdAsync(int id)
        {
            return await _context.Floor.FirstOrDefaultAsync(f => f.Id == id);
        }
        public async Task<IList<Floor>> GetFloorsAsync()
        {
            return await _context.Floor.ToListAsync();
        }
        public async Task<Floor> UpdateFloor(Floor floor)
        {
            _context.Floor.Attach(floor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return floor;
        }
        public async Task<Floor> AddMultiFloor(List<Floor> floor)
        {
            foreach (var f in floor)
            {
            await _context.Floor.AddAsync(f);
            await _context.SaveChangesAsync();

            }
            return null;
        }
    }
}
