using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParkNet_Cristovao.Machado.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkNet_Cristovao.Machado.Data.Repositories
{
    public class VehicleRepository
    {
        private readonly ApplicationDbContext _context;
        public VehicleRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task<List<Vehicle>> GetVehiclesByUserId(string userId)
        {
            return await _context.Vehicle.Where(v => v.User.Id == userId).ToListAsync();
        }
        public async Task<Vehicle> GetVehicleById(int id)
        {
            return await _context.Vehicle.FirstOrDefaultAsync(v => v.Id == id);
        }
        public async Task<Vehicle> AddVehicleAsync(Vehicle vehicle)
        {
            _context.Vehicle.Add(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
        }
        public async Task<Vehicle> DeleteVehicleAsync(int id)
        {
          var vehicle =  GetVehicleById(id).Result;
            _context.Vehicle.Remove(vehicle);

            await _context.SaveChangesAsync();
            return null;
        }
    }
}
