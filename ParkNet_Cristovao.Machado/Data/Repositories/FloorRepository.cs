using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using ParkNet_Cristovao.Machado.Data.Entities;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<List<Floor>> GetFloorsAsync(int parkid)
        {
            return await _context.Floor.Where(f => f.Id == parkid).ToListAsync();
        }
        public async Task<Floor> UpdateFloor(Floor floor)
        {
            _context.Floor.Attach(floor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return floor;
        }
        public async Task<Floor> AddMultiFloor(List<Floor> floor)
        {
            _context.Floor.AddRange(floor);
            return null;
        }
        public List<int> GetFloorByParkId(int id)
        {
          var floors =  _context.Floor.Where(f => f.ParkId == id).ToList();
            List<int> ids = new List<int>();
            foreach (var f in floors)
            {
                ids.Add(f.Id);
            }
            return ids;
        }
        public List<int> GetFloorsId(List<Floor> floors)
        {
            List<int> id = new List<int>();
            foreach (var f in floors)
            {
                id.Add(f.Id);
            }
            return id;
        }
        public async Task<Floor> UpdateFloors(List<Floor> floors)
        {
            foreach (var f in floors)
            {
                _context.Floor.Attach(f).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
            return null;
        }
    }
}
